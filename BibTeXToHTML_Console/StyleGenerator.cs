using System;
using BibTeX;
using BibTeXtoHTML;
using BibToHtml;
using BibManFunctionality;
using BibTeX.Translator;
using BibToHtml.Converter.Styles;

namespace BibTeXToHTML_Console
{
    public class StyleGenerator
    {
        public static void GenerateStyle()
        {
            BibTeXtoHTML.Project.ConversionProject project = 
                new BibTeXtoHTML.Project.ConversionProject ();
            SetBasicTranslatorOptions (project.BibTeXtoHTML_Style.BibTeXTranslator);

            CreateAlphabetFunctions (project.BibTeXtoHTML_Style.BibTeXTranslator);

            TestStyleFullHtml (project.BibTeXtoHTML_Style.HtmlExporterStyle);

            try{
                project.BibTeXtoHTML_Style.WriteThisToXmlFile ("./test.xst");
            }
            catch(Exception ex) {
                Console.WriteLine (ex.ToString());
            }
        }

        #region BibTeX

        public static void SetBasicTranslatorOptions(BibTeX.Translator.BibTeXTranslator translator)
        {
            translator.ExecuteMath = BibTeXTranslator.MathExecutionType.NoIngeretion;
            translator.RemoveBracesInMathMode = false;
        }

        public static void CreateAlphabetFunctions(BibTeX.Translator.BibTeXTranslator translator)
        {
            #region grave `
            //
            // Grave `
            //
            LaTeXFunction_P grave = new LaTeXFunction_P();
            grave.Name = "`";
            grave.DefaultResponse = "\\`{{{0}}}";
            translator.LaTeXFunctions.Add (grave);

            #endregion

            #region Acute '
            //
            // Acute '
            //
            LaTeXFunction_P acute = new LaTeXFunction_P();
            acute.Name = "'";
            acute.DefaultResponse = "\\'{{{0}}}";
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "c", Response = "ć"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "C", Response = "Ć"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "n", Response = "ń"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "N", Response = "Ń"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "o", Response = "ó"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "O", Response = "Ó"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "s", Response = "ś"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "S", Response = "Ś"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "z", Response = "ź"});
            acute.Responses.Add(new LaTeXFunction_P.OneParameterResponse{Input = "Z", Response = "Ź"});
            translator.LaTeXFunctions.Add (acute);

            #endregion

            #region Circumflex ^
            //
            // Circumflex ^
            //
            LaTeXFunction_P circumflex = new LaTeXFunction_P ();
            circumflex.Name = "^";
            circumflex.DefaultResponse = "\\^{{{0}}}";
            translator.LaTeXFunctions.Add(circumflex);

            #endregion

            #region umlaut "
            //
            // umlaut "
            //
            LaTeXFunction_P umlaut = new LaTeXFunction_P();
            umlaut.Name = "\"";
            umlaut.DefaultResponse = "\\\"{{{0}}}";
            translator.LaTeXFunctions.Add(umlaut);

            #endregion

            #region Long hungarian umlaut "
            //
            // Long hungarian umlaut "
            //
            LaTeXFunction_P longUmlaut = new LaTeXFunction_P();
            longUmlaut.Name = "H";
            longUmlaut.DefaultResponse = "\\H{{{0}}}";
            translator.LaTeXFunctions.Add(longUmlaut);

            #endregion

            #region tilde ~
            //
            // tilde ~
            //
            LaTeXFunction_P tilde = new LaTeXFunction_P();
            tilde.Name = "~";
            tilde.DefaultResponse = "\\~{{{0}}}";
            translator.LaTeXFunctions.Add(tilde);

            #endregion

            #region c{c}   ç   cedilla
            //
            // c{c}   ç   cedilla
            //
            LaTeXFunction_P cedilla = new LaTeXFunction_P();
            cedilla.Name = "c";
            cedilla.DefaultResponse = "\\c{{{0}}}";
            translator.LaTeXFunctions.Add (cedilla);

            #endregion

            #region \={o}   ō   macron accent (a bar over the letter)
            //
            // \={o}   ō   macron accent (a bar over the letter)
            //
            LaTeXFunction_P macron = new LaTeXFunction_P();
            macron.Name = "=";
            macron.DefaultResponse = "\\={{{0}}}";
            translator.LaTeXFunctions.Add (macron);

            #endregion

            #region \b{o}   o   bar under the letter
            //
            // \b{o}   o   bar under the letter
            //
            LaTeXFunction_P b = new LaTeXFunction_P();
            b.Name = "b";
            b.DefaultResponse = "\\b{{{0}}}";
            translator.LaTeXFunctions.Add (b);

            #endregion

            #region Ogonek
            //
            // Ogonek
            //
            LaTeXFunction_P k = new LaTeXFunction_P ();
            k.Name = "k";
            k.DefaultResponse = "\\k{{{0}}}";
            k.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "a", Response = "ą" });
            k.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "A", Response = "Ą" });
            k.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "e", Response = "ę" });
            k.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "E", Response = "Ę" });
            translator.LaTeXFunctions.Add (k);

            #endregion

            #region Ł

            //
            // Ł
            //
            LaTeXFunction_N l = new LaTeXFunction_N ();
            l.Name = "l";
            l.DefaultResponse = "ł";
            translator.LaTeXFunctions.Add (l);
            LaTeXFunction_N L = new LaTeXFunction_N ();
            L.Name = "L";
            L.DefaultResponse = "Ł";
            translator.LaTeXFunctions.Add (L);

            #endregion

            #region dot

            //
            // Kropka nad literą
            //
            LaTeXFunction_P dot = new LaTeXFunction_P ();
            dot.Name = ".";
            dot.DefaultResponse = "\\.{{{0}}}";
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "a", Response = "ȧ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "A", Response = "Ȧ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "b", Response = "ḃ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "B", Response = "Ḃ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "c", Response = "ċ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "C", Response = "Ċ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "d", Response = "ḋ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "D", Response = "Ḋ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "e", Response = "ė" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "E", Response = "Ė" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "f", Response = "ḟ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "F", Response = "Ḟ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "g", Response = "ġ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "G", Response = "Ġ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "h", Response = "ḣ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "H", Response = "Ḣ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "I", Response = "İ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "m", Response = "ṁ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "M", Response = "Ṁ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "n", Response = "ṅ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "N", Response = "Ṅ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "o", Response = "ȯ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "O", Response = "Ȯ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "p", Response = "ṗ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "P", Response = "Ṗ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "r", Response = "ṙ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "R", Response = "Ṙ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "s", Response = "ṡ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "S", Response = "Ṡ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "t", Response = "ṫ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "T", Response = "Ṫ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "w", Response = "ẇ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "W", Response = "Ẇ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "x", Response = "ẋ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "X", Response = "Ẋ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "y", Response = "ẏ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "Y", Response = "Ẏ" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "z", Response = "ż" });
            dot.Responses.Add (new LaTeXFunction_P.OneParameterResponse{ Input = "Z", Response = "Ż" });
            translator.LaTeXFunctions.Add (dot);

            #endregion

            #region \d{u}   ụ   dot under the letter
            //
            // \d{u}   ụ   dot under the letter
            //
            LaTeXFunction_P d = new LaTeXFunction_P();
            d.Name = "d";
            d.DefaultResponse = "\\d{{{0}}}";
            translator.LaTeXFunctions.Add (d);

            #endregion

            #region \r{a}   å   ring over the letter (for å there is also the special command \aa)
            //
            // \r{a}   å   ring over the letter (for å there is also the special command \aa)
            //
            LaTeXFunction_P r = new LaTeXFunction_P();
            r.Name = "r";
            r.DefaultResponse = "\\r{{{0}}}";
            translator.LaTeXFunctions.Add (r);

            #endregion

            #region \u{o}   ŏ   breve over the letter
            //
            // \u{o}   ŏ   breve over the letter
            //
            LaTeXFunction_P u = new LaTeXFunction_P();
            u.Name = "u";
            u.DefaultResponse = "\\u{{{0}}}";
            translator.LaTeXFunctions.Add (u);

            #endregion

            #region \v{s}   š   caron/háček ("v") over the letter
            //
            // \v{s}   š   caron/háček ("v") over the letter
            //
            LaTeXFunction_P v = new LaTeXFunction_P();
            v.Name = "v";
            v.DefaultResponse = "\\v{{{0}}}";
            translator.LaTeXFunctions.Add (v);

            #endregion

            #region \t{oo}  o͡o     "tie" (inverted u) over the two letters
            //
            // \t{oo}  o͡o     "tie" (inverted u) over the two letters
            //
            LaTeXFunction_P t = new LaTeXFunction_P();
            t.Name = "t";
            t.DefaultResponse = "\\t{{{0}}}";
            translator.LaTeXFunctions.Add (t);

            #endregion

            #region \o  ø   slashed o (o with stroke)
            //
            // \o  ø   slashed o (o with stroke)
            //
            LaTeXFunction_N o = new LaTeXFunction_N();
            o.Name = "o";
            o.DefaultResponse = "ø";
            translator.LaTeXFunctions.Add (o);
            LaTeXFunction_N O = new LaTeXFunction_N();
            O.Name = "O";
            O.DefaultResponse = "Ø";
            translator.LaTeXFunctions.Add (O);

            #endregion
        }

        #endregion

        #region HTML Exporter

        public static void TestStyleFullHtml(BibStyle style)
        {
            CreateHead (style);
            CreateDefaultPositionStyle (style);
        }

        public static void CreateHead(BibToHtml.Converter.Styles.BibStyle style)
        {
            style.SetHtmlHead = true;
            style.HtmlHead = "<head>\r\n" +
                "<meta charset=\"UTF-8\">\r\n" +
                "<script type=\"text/x-mathjax-config\">\r\n" + 
                "MathJax.Hub.Config({tex2jax: {inlineMath: [['$','$'], ['\\(','\\)']]}});\r\n" +
                "</script>\r\n" +
                "<script type=\"text/javascript\" async\r\n" +
                "src=\"https://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS_CHTML\">" +
                "</script>" +
                "</head>";
        }

        public static void CreateDefaultPositionStyle(BibToHtml.Converter.Styles.BibStyle style)
        {
            PositionStyle defPosStyle = new PositionStyle (PositionStyleType.global);
            defPosStyle.UseIt = true;

            defPosStyle.DefaultFieldStyle = new FieldStyle ();
            defPosStyle.DefaultFieldStyle.UseIt = false;
            defPosStyle.Separator = ", ";

            FieldSortObject fso = new FieldSortObject (FieldSort.ByPositionedFieldStyle);

            PositionedFieldStyle pfs1 = new PositionedFieldStyle ();
            pfs1.FieldsOnPosition.Add ("author");
            pfs1.FieldsOnPosition.Add ("editor");
            pfs1.StyleForThisFieldPosition = new FieldStyle ();
            pfs1.StyleForThisFieldPosition.UseIt = true;
            pfs1.StyleForThisFieldPosition.Tags.Add (SupportedHtmlTags.b);
            fso.PositionedFieldStyles.Add (pfs1);

            PositionedFieldStyle pfs2 = new PositionedFieldStyle ();
            pfs2.FieldsOnPosition.Add ("title");
            pfs2.StyleForThisFieldPosition = new FieldStyle ();
            pfs2.StyleForThisFieldPosition.UseIt = true;
            pfs2.StyleForThisFieldPosition.Prefix = "\"";
            pfs2.StyleForThisFieldPosition.Suffix = "\"";
            fso.PositionedFieldStyles.Add (pfs2);

            PositionedFieldStyle pfs3 = new PositionedFieldStyle ();
            pfs3.FieldsOnPosition.Add("date");
            pfs3.FieldsOnPosition.Add ("year");
            pfs3.StyleForThisFieldPosition = new FieldStyle ();
            pfs1.StyleForThisFieldPosition.UseIt = true;
            fso.PositionedFieldStyles.Add (pfs3);

            defPosStyle.FieldSortObjects.Add (fso);

            style.DefaultPositionStyle = defPosStyle;
        }

        #endregion
    }
}

