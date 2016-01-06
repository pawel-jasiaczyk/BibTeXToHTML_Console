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
            Eksperymenty ();
        }

        static void Eksperymenty ()
        {
            Console.WriteLine ("Eksperymenty");
            ConversionProject project = new ConversionProject ("./Test");

            project.OpenFile ("wos-mariusz.bib");
            project.ProceedBibTeXFiles ();
            project.GetHtml ();
            project.SaveHtml ();
        }
    }
}
