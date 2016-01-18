using System;
using BibTeXtoHTML;
using BibTeXtoHTML.Project;
using BibTeXtoHTML.Style;
using BibTeX;
using BibTeX.Translator;
using BibToHtml;
using BibToHtml.Converter.Styles;

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
        }

        static void FirstConsoleApp (string[] args)
        {
            // Eksperymenty ();
            bool fail = false;
            bool print = false;
            ConversionProject project = new ConversionProject ();
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
                    project.GetHtml ();
                    project.SaveHtml ();
                    if(print)
                        Console.WriteLine(project.HtmlFile.Text);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine (ex.Message);
                }
            }
            else {
                Console.WriteLine ("Musisz podać nazwę pliku wyjśiowego");
            }
        }

        static void SetProject(ConversionProject project)
        {
            AddTestFunctions_N (project.BibTeXDataBase.BibTeXTranslator);
            AddTestFunctions_P (project.BibTeXDataBase.BibTeXTranslator);
            AddTestFunctions_PP (project.BibTeXDataBase.BibTeXTranslator);

//            project.BibTeXDataBase.UseLocalTranslator = false;
//            project.BibToHtmlExporter.Translator = project.BibTeXDataBase.BibTeXTranslator;
//            project.BibToHtmlExporter.UseLocalTranslator = true;

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

            translator.LaTeXFunctions.Add (frac);
        }
    }
}
