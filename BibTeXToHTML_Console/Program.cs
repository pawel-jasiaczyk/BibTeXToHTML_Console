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
            args = FirstConsoleApp ();
        }

        static string[] FirstConsoleApp ()
        {
            string[] args;
            args = new string[] {
                "-a",
                "wos-mariusz.bib",
                "-o",
                "test.html"
            };
            // Eksperymenty ();
            bool fail = false;
            ConversionProject project = new ConversionProject ();
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
                    case "-o": {
                        i++;
                        if (i < args.Length) {
                            project.OutputFileName = args [i];
                            i++;
                        }
                        break;
                    }
                    case "-s": {
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
                project.ProceedBibTeXFiles ();
                project.GetHtml ();
                project.SaveHtml ();
            }
            else {
                Console.WriteLine ("Musisz podać nazwę pliku wyjśiowego");
            }
            return args;
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
    }
}
