using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace CommonTests
{
    public class XsltHelper
    {
        public static void TransformXslt(string xsltPath, string dataPath, string outputPath)
        {

            string data = "<wsop document=\"utdelningsavisering\" xmlns=\"Sodra.Skog.WSOP.UtdelAvis\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Metadata><User>SODRA\\SODRA</User><Organization>Södra</Organization><Company>Skog</Company><Vo /><System>Kapitalhanterings-systemet</System><DateTime>2014-05-15</DateTime><DocumentType>2014</DocumentType><JobId>7696770</JobId><DocumentId>UDE1235445</DocumentId><LevId /><Intressentnummer>194608283331</Intressentnummer><Document><ItrID>194608283331</ItrID><Edt>2014-05-15</Edt></Document></Metadata><Parameters><Language>SE</Language><Archive><Archive>yes</Archive><ArchiveTime>2024-05-16</ArchiveTime></Archive><ExtendedOutput><Extended>yes</Extended><DaysToSave>30</DaysToSave><External>yes</External></ExtendedOutput></Parameters><DocumentData><Hnd><HndDtm>2014-05-16</HndDtm></Hnd><Handl><UsrNam>Systemkonto</UsrNam><UsrSgn>Sys</UsrSgn><UsrTel /><UsrEml /><UsrAvd /><Edt>2014-05-15</Edt></Handl><Itr><ItrID>194608283331</ItrID><Nam>Rolf Nilsson</Nam><ItrKatKod>96</ItrKatKod><PrlSkt>0</PrlSkt><IntEdt>2013-12-20</IntEdt><UtbDirKod>1</UtbDirKod><InsKtoKod>1</InsKtoKod><ElkDirKod>0</ElkDirKod><Adr2>20 Comino Street</Adr2><PstNum>WA</PstNum><PstAdr>6105 CLOVERDALE</PstAdr><Land>Australien</Land></Itr><AviseringUtdelning><Ske><Typ>2</Typ><ItrEf>0</ItrEf><SkeID>169307</SkeID><Nam>Nilsson Brita m fl</Nam><VoID>962</VoID><Adl>1/5</Adl><RgoID>01</RgoID><UtdelningInsats><Kontotyp>ISTI</Kontotyp><PerDatum>2013-12-31</PerDatum><UtdelningsProcent>4.00</UtdelningsProcent><BerakningsSaldo>1560</BerakningsSaldo><Belopp>62</Belopp><OverfortLanekonto>62</OverfortLanekonto><UtbetalatBank>0</UtbetalatBank><Forlagsnr /></UtdelningInsats><UtdelningInsats><Kontotyp>ISTE</Kontotyp><PerDatum>2013-12-31</PerDatum><UtdelningsProcent>4.00</UtdelningsProcent><BerakningsSaldo>4319</BerakningsSaldo><Belopp>173</Belopp><OverfortLanekonto>173</OverfortLanekonto><UtbetalatBank>0</UtbetalatBank><Forlagsnr /></UtdelningInsats><SummaOverfortEF>235</SummaOverfortEF></Ske><Ske><Typ>2</Typ><ItrEf>0</ItrEf><SkeID>926698</SkeID><Nam>Nilsson Brita m fl</Nam><VoID>962</VoID><Adl>1/5</Adl><RgoID>01</RgoID><UtdelningInsats><Kontotyp>ISTI</Kontotyp><PerDatum>2013-12-31</PerDatum><UtdelningsProcent>4.00</UtdelningsProcent><BerakningsSaldo>3720</BerakningsSaldo><Belopp>149</Belopp><OverfortLanekonto>149</OverfortLanekonto><UtbetalatBank>0</UtbetalatBank><Forlagsnr /></UtdelningInsats><UtdelningInsats><Kontotyp>ISTE</Kontotyp><PerDatum>2013-12-31</PerDatum><UtdelningsProcent>4.00</UtdelningsProcent><BerakningsSaldo>12845</BerakningsSaldo><Belopp>514</Belopp><OverfortLanekonto>514</OverfortLanekonto><UtbetalatBank>0</UtbetalatBank><Forlagsnr /></UtdelningInsats><SummaOverfortEF>663</SummaOverfortEF></Ske><TotSummaOverfortLanekonto>0</TotSummaOverfortLanekonto><TotSummaUtbetalatBank>0</TotSummaUtbetalatBank><Regioner><Region><Namn>Syd</Namn><Adr1>Södra Cell Mörrum</Adr1><PstNum>375 86</PstNum><PstAdr>Mörrum</PstAdr><Epost /><Telnr>0454-55570</Telnr><Fax /></Region></Regioner></AviseringUtdelning></DocumentData></wsop>";


            string data2 = @"<wsop document=""utdelningsavisering"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <Metadata>
    <User>SODRA\SODRA</User>
    <Organization>Södra</Organization>
    <Company>Skog</Company>
    <Vo></Vo>
    <System>Kapitalhanterings-systemet</System>
    <DateTime>2015-04-28</DateTime>
    <DocumentType>2015</DocumentType>
    <JobId>8577373</JobId>
    <DocumentId>UDE1362716</DocumentId>
    <LevId />
    <Intressentnummer>197701242708</Intressentnummer>
    <Document>
      <ItrID>197701242708</ItrID>
      <Edt>2015-04-28</Edt>
    </Document>
  </Metadata>
  <Parameters>
    <Language>SE</Language>
    <Archive>
      <Archive>yes</Archive>
      <ArchiveTime>2025-04-28</ArchiveTime>
    </Archive>
    <ExtendedOutput>
      <Extended>yes</Extended>
      <DaysToSave>30</DaysToSave>
      <External>yes</External>
    </ExtendedOutput>
  </Parameters>
  <DocumentData>
    <Hnd>
      <HndDtm>2015-04-28</HndDtm>
    </Hnd>
    <Handl>
      <UsrNam>Systemkonto</UsrNam>
      <UsrSgn>Sys</UsrSgn>
      <UsrTel />
      <UsrEml />
      <UsrAvd />
      <Edt>2015-04-28</Edt>
    </Handl>
    <Itr>
      <ItrID>197701242708</ItrID>
      <Nam>Martina Svensson</Nam>
      <ItrKatKod>96</ItrKatKod>
      <PrlSkt>0</PrlSkt>
      <IntEdt>1994-01-01</IntEdt>
      <UtbDirKod>1</UtbDirKod>
      <InsKtoKod>1</InsKtoKod>
      <ElkDirKod>0</ElkDirKod>
      <Adr2>Skogsduvevägen 17</Adr2>
      <PstNum>43853</PstNum>
      <PstAdr>HINDÅS</PstAdr>
    </Itr>
    <AviseringUtdelning>
      <Ske>
        <Typ>2</Typ>
        <ItrEf>0</ItrEf>
        <SkeID>436135</SkeID>
        <Nam>Svensson Carl-Olof m fl</Nam>
        <VoID>965</VoID>
        <Adl>1/3</Adl>
        <RgoID>01</RgoID>
        <UtdelningInsats>
          <Kontotyp>ISTI</Kontotyp>
          <PerDatum>2014-12-31</PerDatum>
          <UtdelningsProcent>6.00</UtdelningsProcent>
          <BerakningsSaldo>1733</BerakningsSaldo>
          <Belopp>104</Belopp>
          <OverfortLanekonto>104</OverfortLanekonto>
          <UtbetalatBank>0</UtbetalatBank>
          <Forlagsnr />
        </UtdelningInsats>
        <UtdelningInsats>
          <Kontotyp>ISTE</Kontotyp>
          <PerDatum>2014-12-31</PerDatum>
          <UtdelningsProcent>6.00</UtdelningsProcent>
          <BerakningsSaldo>1544</BerakningsSaldo>
          <Belopp>93</Belopp>
          <OverfortLanekonto>93</OverfortLanekonto>
          <UtbetalatBank>0</UtbetalatBank>
          <Forlagsnr />
        </UtdelningInsats>
        <SummaOverfortEF>197</SummaOverfortEF>
        <Emission>
          <EmissionsProcent>5.00</EmissionsProcent>
          <Belopp>87</Belopp>
        </Emission>
      </Ske>
      <TotSummaOverfortLanekonto>0</TotSummaOverfortLanekonto>
      <TotSummaUtbetalatBank>0</TotSummaUtbetalatBank>
      <Regioner>
        <Region>
          <Namn>Syd</Namn>
          <Adr1>Södra Cell Mörrum</Adr1>
          <PstNum>375 86</PstNum>
          <PstAdr>Mörrum</PstAdr>
          <Epost />
          <Telnr>0454-55570</Telnr>
          <Fax />
        </Region>
      </Regioner>
    </AviseringUtdelning>
  </DocumentData>
</wsop>";
            
            
            //XslCompiledTransform xslTrans = new XslCompiledTransform();
            XslTransform xslTrans = new XslTransform();
            xslTrans.Load(xsltPath);

            XPathDocument doc;      // = new XPathDocument(dataPath);
            
            using (StringReader stringReader = new StringReader(data))
            {
                doc = new XPathDocument(stringReader);
            }

            using (XmlTextWriter result = new XmlTextWriter(outputPath, Encoding.UTF8))
            {
                result.Formatting = Formatting.Indented;
                xslTrans.Transform(doc, null, result, new XmlUrlResolver());
            }

            //string textOutput;


            //using (MemoryStream result = new MemoryStream())
            //{
            //    xslTrans.Transform(doc, null, result, new XmlUrlResolver());

            //    result.Position = 0;

            //    using (StreamReader reader = new StreamReader(result, Encoding.GetEncoding("utf-8")))
            //    {
            //        textOutput = reader.ReadToEnd();
            //    }
            //}

       }
    }
}
