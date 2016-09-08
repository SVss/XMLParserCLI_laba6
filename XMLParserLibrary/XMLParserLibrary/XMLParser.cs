using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XMLParserLibrary
{
    public class XMLNode
    {
        private string m_Name;
        private string m_Value;

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public string Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }

        public XMLNode(string name, string value = null)
        {
            this.Name = name;
            this.Value = value;
        }

        public bool hasValue()
        {
            return (m_Value != null);
        }
    }

    public class XMLAttribute : XMLNode
    {
        public XMLAttribute(string name, string value = null) : base(name, value) { }
    }

    public class XMLText : XMLNode
    {
        public XMLText(string text = "") : base("text", text) {}

        public void Append(string value)
        {
            this.Value += value;
        }

        public void setText(string value)
        {
            this.Value = value;
        }

        internal bool Empty()
        {
            return (Value == "");
        }
    }

    public class XMLElement : XMLNode
    {
        private List<XMLAttribute> m_Attributes = new List<XMLAttribute>();
        private List<XMLElement> m_Children = new List<XMLElement>();
        private XMLText m_Text = new XMLText();

        public XMLElement(string name, string value = null) : base(name, value) {}

        public bool addChild(XMLElement newChild)
        {
            bool result = false;

            m_Children.Add(newChild);
            result = true;

            return result;
        }

        public void removeChildAt(int i)
        {
            this.m_Children.RemoveAt(i);
        }

        public bool hasChildren()
        {
            return (this.m_Children.Count > 0);
        }

        public List<XMLElement> getChildrenList()
        {
            return this.m_Children;
        }


        public bool addAttribute(XMLAttribute newAttribute)
        {
            bool result = false;

            int prevPos = m_Attributes.FindIndex( attr => { return (attr.Name == newAttribute.Name); } );
            if (prevPos > -1)
            {
                m_Attributes.RemoveAt(prevPos);
            }

            m_Attributes.Add(newAttribute);
            result = true;

            return result;
        }

        public bool hasAttribute(string name)
        {
            return (m_Attributes.FindIndex(attr => { return (attr.Name == name); }) > -1);
        }

        public void removeAttributeAt(int i)
        {
            this.m_Attributes.RemoveAt(i);
        }

        public void setAttributes(List<XMLAttribute> attrs)
        {
            this.m_Attributes = attrs;
        }

        public bool hasAttributes()
        {
            return (m_Attributes.Count > 0);
        }

        public List<XMLAttribute> getAttributesList()
        {
            return this.m_Attributes;
        }


        public void appendText(string text)
        {
            m_Text.Append(text);
        }

        public void setText(string value)
        {
            this.m_Text.setText(value);
        }

        public string getText()
        {
            return this.m_Text.Value;
        }

        public void removeText()
        {
            this.m_Text.setText("");
        }

        public bool hasText()
        {
            return (!this.m_Text.Empty());
        }


        public List<XMLNode> getNodesList()
        {
            var result = new List<XMLNode>();

            result.AddRange(this.m_Attributes.ToArray());
            result.AddRange(this.m_Children.ToArray());

            if (!m_Text.Empty())
            {
                result.Add(this.m_Text);
            }

            return result;
        }

    }

    public class XMLParser
    {
        private Regex RE_XML_COMMENT = new Regex("<!--(.*?)-->", RegexOptions.Multiline);
        private Regex RE_XML_ROOT = new Regex(@"<\?xml(.*?)\?>", RegexOptions.Multiline);

        private Regex RE_XML_TAG = new Regex(@"<(.*?)>", RegexOptions.Multiline);
        private Regex RE_XML_TAG_CLOSE = new Regex("</(.*?)>", RegexOptions.Multiline);
        private Regex RE_XML_TAG_PARTS = new Regex(@"<(.*?)(?:\s(.*)?)?>", RegexOptions.Multiline);   // name: g1, attrs: g2, self-closing: g3
        private Regex RE_XML_TAG_SELFCLOSE = new Regex("<(.*?)/>", RegexOptions.Multiline);

        private Regex RE_XML_ATTRIBUTES = new Regex("(.*?)=(\".*?\"|'.*?')", RegexOptions.Multiline); // name: g1, value: g2
        private Regex RE_XML_ATTR_VALUE_CLEAR = new Regex("['\"](.*?)['\"]");


        private string m_ProcString;

        private string clearAttr(string attr)
        {
            return RE_XML_ATTR_VALUE_CLEAR.Match(attr).Groups[1].Value;
        }

        private List<XMLAttribute> parseAttributes(string attr)
        {
            var result = new List<XMLAttribute>();

            var match = RE_XML_ATTRIBUTES.Match(attr);
            while (match.Success)
            {
                result.Add(new XMLAttribute(match.Groups[1].Value, clearAttr(match.Groups[2].Value)));
                match = match.NextMatch();
            }

            return result;
        }

        private XMLElement processBlock(Match tag, ref int index)
        {
            XMLElement result = new XMLElement("");

            string tagStr = tag.Value;

            if (!RE_XML_TAG_CLOSE.Match(tagStr).Success)
            {
                var tagMatch = RE_XML_TAG_PARTS.Match(tagStr);

                string name = tagMatch.Groups[1].Value;
                bool selfClose = RE_XML_TAG_SELFCLOSE.Match(tagStr).Success;

                string attrs = tagMatch.Groups[2].Value;
                var attributes = parseAttributes(attrs);

                result.Name = name;
                result.setAttributes(attributes);

                index += tagMatch.Index + tagMatch.Length;

                if (!selfClose)
                {
                    string textPart = "";

                    bool closed = false;
                    while ((!closed) && ((tag = nextTagMatch(index)).Success))
                    {
                        textPart = m_ProcString.Substring(index, tag.Index - index);
                        result.appendText(textPart.Trim());

                        if ((tagMatch = RE_XML_TAG_CLOSE.Match(tag.Value)).Success)   // if closing tag
                        {
                            if (tagMatch.Groups[1].Value == name)   // for this entity
                            {
                                closed = true;  // exit
                                index = tag.Index + tag.Length;  // pass last position to caller
                            }
                            else    // not this entity
                            {
                                throw new Exception("XMLParser error: Intersection of closing tags at symbol "
                                    + tag.Index.ToString() + ".");
                            }
                        }
                        else    // opening tag
                        {
                            index = tag.Index;
                            result.addChild(processBlock(tag, ref index));
                        }
                    }
                }
            }
            else
            {
                throw new Exception("XMLParser error: Closing tag before opening at symbol "
                    + tag.Index.ToString() + ".");
            }

            return result;
        }

        private Match nextTagMatch(int pos = 0)
        {
            return RE_XML_TAG.Match(m_ProcString, pos);
        }

        private XMLElement processDocument(bool checkPrologue = false)
        {
            int startIndex = 0;

            if (checkPrologue)
            {

                var match = RE_XML_ROOT.Match(m_ProcString);    // check prologue
                if ((!match.Success) || (match.Index > 0))
                {
                    Console.WriteLine("Invalid XML prologue.");
                }

                startIndex = match.Index + match.Length;
            }

            Match rootTag = nextTagMatch(startIndex);
            startIndex = rootTag.Index;

            XMLElement doc = processBlock(rootTag, ref startIndex);

            return doc;
        }

        private string clearComments(string input)
        {
            return RE_XML_COMMENT.Replace(input, "");
        }

        private string clearInput(string input)
        {
            return clearComments(input.Replace('\n',' ').Replace('\r', ' '));
        }

        public XMLElement parse(string inputString, bool checkPrologue = false)
        {
            this.m_ProcString = clearInput(inputString);

            XMLElement doc = processDocument(checkPrologue);
            return doc;
        }

    }   // XMLParser

    
    public class XMLAssembler
    {
        private string getBlock(XMLElement node)
        {
            string result = "<" + node.Name;

            if (node.hasAttributes())
            {
                foreach(var attr in node.getAttributesList())
                {
                    result += " " + attr.Name + "=" + "\"" + attr.Value + "\"";
                }
            }

            if (!(node.hasText() || node.hasChildren()))
            {
                result += "/>";
            }
            else
            {
                result += ">";

                if (node.hasText())
                {
                    result += node.getText();
                }

                if (node.hasChildren())
                {
                    foreach (var child in node.getChildrenList())
                    {
                        result += getBlock(child);
                    }
                }

                result += "</" + node.Name + ">";
            }

            return result;
        }

        public string assemble(XMLElement root)
        {
            string result = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + getBlock(root);
            return result;
        }

    }   // XMLAssembler

}   // namespase XMLParser
