// XMLParserTestCppCLI.cpp: главный файл проекта.

#include "stdafx.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::IO;
using namespace XMLParserLibrary;

String^ readFile(String^ fileName)
{
	String^ result = nullptr;

	try 
	{
		Console::WriteLine("Trying to open file {0}...", fileName);
		StreamReader^ din = File::OpenText(fileName);

		String^ str;
		while ((str = din->ReadLine()) != nullptr) 
		{
			result += "\n" + str;
		}
	}
	catch (Exception^ e)
	{
		result = nullptr;

		if (dynamic_cast<FileNotFoundException^>(e))
			Console::WriteLine("File '{0}' not found", fileName);
	}

	return result;
}

bool goDeeper(XMLElement^ node)
        {
            bool result = false;

            if (node->hasChildren())
            {
                List<XMLElement^>^ chList = node->getChildrenList();
                XMLElement^ child;

                for(int i = 0; i < chList->Count; ++i)
                {
                    child = chList[i];
                    if (goDeeper(child))
                    {
                        if (!node->hasAttribute(child->Name))
                        {
                            node->addAttribute(gcnew XMLAttribute(child->Name, child->getText()));
                            node->removeChildAt(i);
                            --i;    // to keep it normal
                        }
                    }
                }
            }
            else
            {
                if (!node->hasAttributes())
                {
                    result = true;
                }
            }

            return result;
        }


String^ processXml(String^ xmlDocument)
{
	XMLParser^ xmlParser = gcnew XMLParser();
	XMLElement^ root = xmlParser->parse(xmlDocument);
	
	goDeeper(root);

	XMLAssembler^ xmlAsm = gcnew XMLAssembler();
	String^ result = xmlAsm->assemble(root);

	return result;
}

int main(array<System::String ^> ^args)
{
	String^ outputFileName = "out.xml";

	if (args->Length < 1)
	{
		Console::WriteLine(L"Need to have at least 1 argument");
		Console::WriteLine(L" 1st - input XML filename");
		Console::WriteLine(L" 2nd [optional] - output filename");
		Console::WriteLine(L"\t(\"output.xml\" by default)");

		Console::ReadLine();
		return -1;
	}

	String^ fileName = args[0];
	Console::WriteLine("Input filename: '{0}'\n", fileName);

	if (args->Length > 1)
	{
		outputFileName = args[1];
		Console::WriteLine("Output filename: '{0}'\n", outputFileName);
	}

	String^ xmlText = readFile(fileName);

	if (xmlText != nullptr)
	{
		Console::WriteLine("File loaded!");

		Console::WriteLine("Processing xml...");
		String^ processedXmlText = processXml(xmlText);

		Console::WriteLine("Writing output file...");
		System::IO::File::WriteAllText(outputFileName, processedXmlText, System::Text::Encoding::UTF8);
	}
	else
	{
		Console::WriteLine("Problem reading file '{0}'\n", fileName);
	}

	Console::WriteLine("Done!");
	Console::ReadLine();
	return 0;
}
