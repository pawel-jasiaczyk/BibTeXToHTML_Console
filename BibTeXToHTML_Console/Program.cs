using System;
using BibTeXtoHTML;
using BibTeXtoHTML.Project;
using BibTeXtoHTML.Style;
using BibTeX;
using BibTeX.Translator;
using BibToHtml;
using BibToHtml.Converter.Styles;

using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace BibTeXToHTML_Console
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            args = new string[] {
                "-a",
                "wos-mariusz_blad.bib",
                "-o",
                "test.html",
                "-p"
            };
            FirstConsoleApp (args);
            // CloneTest();
            // StyleGenerator.GenerateStyle();
            //serializationTest();

            Console.ReadLine();
        }

        static void serializationTest()
        {
            BibTeXTranslator tr = new BibTeXTranslator ();
            //StyleGenerator.CreateAlphabetFunctions (tr);
            // tr.LaTeXFunctions.Add(new LaTeXFunction_N{Name = "test"});
            // tr.LaTeXFunctions.Add(new LaTeXFunction_P{Name = "test2"});

            LaTeXFunction_N N = new LaTeXFunction_N ();
            N.Name = "N";

            tr.LaTeXFunctions.Add (N);

            string path = "./test.xml";
            XmlSerializer ser = new XmlSerializer(typeof(BibTeXTranslator));
            TextWriter wr = new StreamWriter(path);
            try 
            {
                ser.Serialize(wr, tr); 
            }
            catch (Exception ex)
            { 
                Console.WriteLine (ex.ToString());
            }
            finally { wr.Close(); }
        }

        static void CloneTest()
        {
            LaTeXFunction_MathP p = new LaTeXFunction_MathP ();
            p.Name = "p";
            p.DefaultResponse = "p";
            LaTeXFunction_MathP.OneParameterResponse n = new LaTeXFunction_MathP.OneParameterResponse ();
            n.Input = "test";
            n.Response = "wynik";
            p.Responses.Add (n);
            try
            {
                Console.WriteLine ("{0},{1},{2},{3}", 
                    p.Name, p.DefaultResponse, p.Responses[0].Input, p.Responses[0].Response);
            }
            catch(Exception ex) 
            {
                Console.WriteLine (ex.Message);
            }
            LaTeXFunction_MathP copy = p.CloneAsType () as LaTeXFunction_MathP;
            try
            {
                Console.WriteLine ("{0},{1},{2},{3}", 
                    copy.Name, copy.DefaultResponse, copy.Responses[0].Input, copy.Responses[0].Response);
            }
            catch(Exception ex) 
            {
                Console.WriteLine (ex.Message);
            }
        }

        static void FirstConsoleApp (string[] args)
        {
            // Eksperymenty ();
            bool fail = false;
            bool print = false;
            ConversionProject project = new ConversionProject ();
            // SetTestStyle (project);
            SetProject (project);


            for (int i = 0; i < args.Length; i++) {
                if (fail)
                    break;
                if (args [i].StartsWith ("-")) {
                    switch (args [i]) {
                    case "-a": {
                        i++;
                        for (; i < args.Length; i++) {
                            if (args [i].StartsWith ("-")) {
                                i--;
                                break;
                            }
                            try {
                                project.OpenFile (args [i]);
                            }
                            catch (Exception ex) {
                                Console.WriteLine (ex.Message);
                            }
                        }
                        break;
                    }
                    case "-o": 
                    {
                        i++;
                        if (i < args.Length) {
                            project.OutputFileName = args [i];
                        }
                        break;
                    }
                    case "-s": 
                    {
                        break;
                    }
                    case "-p":
                        {
                            print = true;
                            break;
                        }
                    default: {
                        Console.WriteLine ("Nieznane polecenie {0}", args [i]);
                        fail = true;
                        break;
                    }
                    }
                }
                else {
                    Console.WriteLine ("Nieznane polecenie {0}", args [i]);
                    break;
                }
            }
            if (project.OutputFileName != null) {
                try
                {
                    project.ProceedBibTeXFiles ();
                    project.ExecuteStyle();
                    project.GetHtml ();
                    project.SaveHtml ();
                    if(print)
                        Console.WriteLine(project.HtmlFile.Text);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine (ex.ToString());
                }
            }
            else {
                Console.WriteLine ("Musisz podać nazwę pliku wyjśiowego");
            }
            foreach (Exception ex in project.ExceptoptionsLog.Exceptions) 
            {
                Console.WriteLine (ex.Message);
            }
        }

        static void SetProject(ConversionProject project)
        {
//            AddTestFunctions_N (project.BibTeXtoHTML_Style.BibTeXTranslator);
//            AddTestFunctions_P (project.BibTeXtoHTML_Style.BibTeXTranslator);
//            AddTestFunctions_PP (project.BibTeXtoHTML_Style.BibTeXTranslator);
//            AddTestMathFunctions_P (project.BibTeXtoHTML_Style.BibTeXTranslator);
//            AddTestFunctions_OP(project.BibTeXtoHTML_Style.BibTeXTranslator);
            project.BibTeXtoHTML_Style = BibTeXtoHTML_Style.ReadFromFile("./test.xst");
            project.BibTeXtoHTML_Style.ForceChanges ();
        }

        static void Eksperymenty ()
        {
            Console.WriteLine ("Eksperymenty");
            ConversionProject project = new ConversionProject ();
            project.OutputFileName = "result.html";
            project.OpenFile ("wos-mariusz.bib");
            project.ProceedBibTeXFiles ();
            project.GetHtml ();
            project.SaveHtml ();
        }

        static void AddTestFunctions_N(BibTeX.Translator.BibTeXTranslator translator)
        {
            LaTeXFunction_N textbraceleft = new LaTeXFunction_N ();
            textbraceleft.DefaultResponse = "{";
            textbraceleft.Name = "textbraceleft";

            LaTeXFunction_N textbraceright = new LaTeXFunction_N ();
            textbraceright.DefaultResponse = "}";
            textbraceright.Name = "textbraceright";

            LaTeXFunction_N l = new LaTeXFunction_N ();
            l.Name = "l";
            l.DefaultResponse = "ł";

            translator.LaTeXFunctions.Add (textbraceleft);
            translator.LaTeXFunctions.Add (textbraceright);
            translator.LaTeXFunctions.Add (l);

            LaTeXFunction_MathN cdot = new LaTeXFunction_MathN ();
            cdot.Name = "cdot";
            cdot.DefaultResponse = "&middot;";
            translator.MathFunctions.Add (cdot);
        }

        static void AddTestFunctions_P(BibTeX.Translator.BibTeXTranslator translator)
        {
            LaTeXFunction_P mbox = new LaTeXFunction_P ();
            mbox.DefaultResponse = "{0}";
            mbox.Name = "mbox";

            LaTeXFunction_P k = new LaTeXFunction_P ();
            k.Name = "k";
            k.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "a", Response = "ą" });

            translator.LaTeXFunctions.Add (mbox);
            translator.LaTeXFunctions.Add (k);
        }

        static void AddTestFunctions_PP (BibTeX.Translator.BibTeXTranslator translator)
        {
            LaTeXFunction_PP frac = new LaTeXFunction_PP ();
            frac.Name = "frac";
            frac.DefaultResponse = "({0})/({1}) <p>";

            //translator.LaTeXFunctions.Add (frac);
        }

        static void AddTestMathFunctions_P(BibTeX.Translator.BibTeXTranslator translator)
        {
            LaTeXFunction_MathNoSlash sup = new LaTeXFunction_MathNoSlash ();
            sup.Name = "^";
            sup.DefaultResponse = "<small><sup><sup>{0}</sup></sup></small>";

            LaTeXFunction_MathNoSlash sub = new LaTeXFunction_MathNoSlash ();
            sub.Name = "_";
            sub.DefaultResponse = "<sub><sub>{0}</sub></sub>";

            LaTeXFunction_MathPP frac = new LaTeXFunction_MathPP ();
            frac.Name = "frac";
            frac.DefaultResponse = 
                "<table style=\"display:inline-table; border-collapse: collapse; \">" +
                    "<tr style=\"border-bottom:1px solid black;\">" +
                        "<td style=\"text-align:center;\">{0}</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td style=\"text-align:center;\">{1}</td>" +
                    "</tr>" +
                "</table>";

            translator.MathFunctions.Add (sup);
            translator.MathFunctions.Add (sub);
            translator.MathFunctions.Add (frac);
        }

        static void AddTestFunctions_OP(BibTeX.Translator.BibTeXTranslator translator)
        {
            LaTeXFunction_OP test = new LaTeXFunction_OP ();
            test.DefaultResponse = "argument:{0}";
            test.DefaultResponseWithOptionalContent = "argument:{0}, opArg:{1}";
            test.Name = "test";

            translator.LaTeXFunctions.Add (test);
        }

        static void SetTestStyle(ConversionProject project)
        {
            FieldStyle test = new FieldStyle ("title");
            test.Prefix = "W książce: \"";
            test.Suffix = "\"";
            test.Tags.Add (SupportedHtmlTags.b);
            project.BibTeXtoHTML_Style.HtmlExporterStyle.DefaultPositionStyle.FieldStyles.Add (test);
            project.BibTeXtoHTML_Style.HtmlExporterStyle.DefaultPositionStyle.Separator = ", ";
            project.BibTeXtoHTML_Style.HtmlExporterStyle.DefaultPositionStyle.ParagraphParameters = 
                " style=\"display:inline-table;\"";
            project.BibTeXtoHTML_Style.HtmlExporterStyle.SetHtmlHead = true;
            project.BibTeXtoHTML_Style.HtmlExporterStyle.HtmlHead = "<head>\r\n" +
                    "<meta charset=\"UTF-8\">\r\n" +
                    "<script type=\"text/x-mathjax-config\">\r\n" + 
                        "MathJax.Hub.Config({tex2jax: {inlineMath: [['$','$'], ['\\(','\\)']]}});\r\n" +
                    "</script>\r\n" +
                    "<script type=\"text/javascript\" async\r\n" +
                        "src=\"https://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS_CHTML\">" +
                    "</script>" +
                "</head>";

            project.BibTeXtoHTML_Style.BibTeXTranslator.ExecuteMath = 
                BibTeXTranslator.MathExecutionType.NoIngeretion;
            project.BibTeXtoHTML_Style.BibTeXTranslator.RemoveBracesInMathMode = false;

            project.BibTeXtoHTML_Style.ForceChanges ();
        }
    }
}
