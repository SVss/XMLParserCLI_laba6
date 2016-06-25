using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XMLParserLibrary;

namespace XMLParserTest
{
    public partial class MainWindow : Form
    {
        private XMLElement xmlRoot;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void formTree(XMLElement root, ref TreeNode treeNodes)
        {
            foreach (var node in root.getNodesList())
            {
                TreeNode newTreeNode = treeNodes.Nodes.Add(node.Name);
                newTreeNode.Tag = node;

                if (node.hasValue())
                {
                    newTreeNode.Nodes.Add(node.Value);
                }

                if (node is XMLElement)
                {
                    formTree((node as XMLElement), ref newTreeNode);
                }
            }
        }

        private void showXmlRoot()
        {
            this.treeViewDoc.Nodes.Clear();
            TreeNode xmlNode = treeViewDoc.Nodes.Add(xmlRoot.Name);
            formTree(xmlRoot, ref xmlNode);

            treeViewDoc.ExpandAll();
            treeViewDoc.Nodes[0].EnsureVisible();
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            XMLParser xmlParser = new XMLParser();
            xmlRoot = xmlParser.parse(this.textBoxInput.Text);

            showXmlRoot();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogInput.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxInput.Text = System.IO.File.ReadAllText(openFileDialogInput.FileName);
                treeViewDoc.Nodes.Clear();
            }
        }

        private bool goDeeper(XMLElement node)
        {
            bool result = false;

            if (node.hasChildren())
            {
                List<XMLElement> chList = node.getChildrenList();
                XMLElement child;

                for(int i = 0; i < chList.Count; ++i)
                {
                    child = chList[i];
                    if (goDeeper(child))
                    {
                        if (!node.hasAttribute(child.Name))
                        {
                            node.addAttribute(new XMLAttribute(child.Name, child.getText()));
                            node.removeChildAt(i);
                            --i;    // to keep it normal
                        }
                    }
                }

                var newChild = new XMLElement("newChild");
                newChild.addAttribute(new XMLAttribute("status", "added"));

                node.addChild(newChild);
            }
            else
            {
                if (!node.hasAttributes())
                {
                    result = true;
                }
            }

            return result;
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            goDeeper(xmlRoot);

            XMLAssembler xmlAsm = new XMLAssembler();
            string result = xmlAsm.assemble(xmlRoot);

            textBoxInput.Text = result;
            showXmlRoot();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialogOutput.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialogOutput.FileName, textBoxInput.Text);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxInput.Clear();
            treeViewDoc.Nodes.Clear();

            xmlRoot = null;
        }

    }

}
