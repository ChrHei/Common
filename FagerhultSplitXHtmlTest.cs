using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using EPiServer.Core;
using System.Collections.Generic;

namespace CommonTests
{
    [TestClass]
    public class FagerhultSplitXHtmlTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void FagerhultSplitXHtml()
        {
            List<string> strings = new List<string>();

            strings.Add("");
            strings.Add("<p>This is a paragraph</p>");
            strings.Add(@"
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
            ");
            strings.Add(@"
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
            ");
            strings.Add(@"
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
            ");
            strings.Add(@"
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
                <p>This is a paragraph</p>
            ");

            strings.Add(@"
<p><strong>Installation</strong><br />Surface mounted, cable wire, wire suspension or horizontally on a wall. Installation using the seal supplied with the luminaire provides protection classification IP 44. Without seals the luminaire is classified IP 20. Single lamp luminaires are not balanced therefore cable wire installations should be avoided.</p>
<p><strong>Connection</strong><br />Four Ø 19 mm access holes with blanking grommets on top of the luminaire and one Ø 16 mm knockout in each end. The centrally positioned snap-in terminal block 5x2.5 mm² provides through-wiring. The luminaire is also available with 5x1.5 mm² through-wiring and snap-in terminal blocks at each end (-143).</p>
<p><strong>Design</strong><br />Body in white enamelled, corrosion resistant aluzinc (RAL 9016). End caps of ASA plastic.</p>
<p><strong>Louvre</strong><br />Clear cover of extruded acrylic plastic with prismatic underside and lined sides.</p>
<p><strong>Emergency lighting</strong><br />AllFive with emLED available in 1x25/28 W and 1x32/35 W. For further information on emLED please refer to the Emergency Lighting chapter. When the seal supplied with the luminaire is used, the protection classification is IP 23. Without seals the luminaire is classified IP 20.</p>
<p><strong>Dimming</strong><br /><em>e-Sense Detect</em> – microwave sensor for on/off function or absence dampening 10–100 %. For further information please refer to the dimming section in the Technical Information. <br /><em>e-Sense Move </em>– wireless lighting control between luminaires. Microwave sensor for on/off function, or absence dimming with the option of switch-off function.</p>
<p><strong>Accessories</strong><br />A reflector of specular anodised aluminium is available as an accessory for the 2-lamp version. Can be installed for a symmetrical or asymmetrical light distribution.</p>
<p><strong>Miscellaneous</strong><br />Reflectors are available as accessories for the two lamp luminaires.</p>
");

            foreach (string s in strings)
            {
                Parse(s);
            }
        }

        private void Parse(string value)
        {
            MatchCollection matches = Regex.Matches(value, "<p.?>.+?</p>");
            float total = matches.Count;

            TestContext.WriteLine("Found {0} matches for string '{1}'", total, value);


            if (total > 0)
            {
                // if odd number of paragraphs, make sure the first column contains one more
                int first = Convert.ToInt32(total / 2 + (total % 2 == 0 ? 0 : 0.5));
                string s1 = value.Substring(0, matches[first - 1].Index + matches[first - 1].Length);

                // the second column contains the rest
                string s2 = value.Substring(s1.Length, value.Length - s1.Length);
            }
            else 
            {
            }
        }

    }
}
