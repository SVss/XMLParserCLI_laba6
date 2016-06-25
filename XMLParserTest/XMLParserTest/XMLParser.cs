using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XMLParser
{
    class XMLNode
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

        internal bool hasValue()
        {
            return (m_Value != null);
        }
    }

    class XMLAttribute : XMLNode
    {
        public XMLAttribute(string name, string value = null) : base(name, value) { }
    }

    class XMLText : XMLNode
    {
        public XMLText(string text = "") : base("text", text) {}

        public void Append(string value)
        {
            this.Value += value;
        }

        internal bool Empty()
        {
            return Value == "";
        }
    }

    class XMLElement : XMLNode
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

        public void setAttributes(List<XMLAttribute> attrs)
        {
            this.m_Attributes = attrs;
        }

        public void appendText(string text)
        {
            m_Text.Append(text);
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

    class XMLParser
    {
        private Regex RE_XML_COMMENT = new Regex("<!--(.*?)-->");
        private Regex RE_XML_ROOT = new Regex(@"<\?xml(.*?)\?>");

        private Regex RE_XML_TAG = new Regex(@"<(.*?)>");
        private Regex RE_XML_TAG_CLOSE = new Regex("</(.*?)>");
        private Regex RE_XML_TAG_PARTS = new Regex(@"<(.*?)(?:\s(.*)?)?>");   // name: g1, attrs: g2, self-closing: g3
        private Regex RE_XML_TAG_SELFCLOSE = new Regex("<(.*?)/>");

        private Regex RE_XML_ATTRIBUTES = new Regex("(.*?)=(\".*?\"|'.*?')"); // name: g1, value: g2
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

        private XMLElement processDocument()
        {
            XMLElement doc = new XMLElement("xml");

            var match = RE_XML_ROOT.Match(m_ProcString);    // check prologue
            if ( (!match.Success) || (match.Index > 0) )
            {
                throw new Exception("Not a valid XML prologue.");
            }

            int startIndex = match.Index + match.Length;

            Match rootTag = nextTagMatch(startIndex);
            startIndex = rootTag.Index;

            XMLElement root = processBlock(rootTag, ref startIndex);
            doc.addChild(root);

            return doc;
        }

        private string clearComments(string input)
        {
            return RE_XML_COMMENT.Replace(input, "");
        }

        private string clearInput(string input)
        {
            return clearComments(input);
        }

        public XMLElement parse(string inputString)
        {
            this.m_ProcString = clearInput(inputString);

            XMLElement doc = processDocument();
            return doc;
        }

    }   // XMLParser

}   // namespase XMLParser
