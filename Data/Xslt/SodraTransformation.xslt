<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs">
  <xsl:output method="xml" encoding="utf-8" indent="yes"/>
  <xsl:template match="/wsop">
    <wsop>
      <Metadata>
        <xsl:for-each select="User">
          <User>
            <xsl:value-of select="."/>
          </User>
        </xsl:for-each>
        <xsl:for-each select="Organization">
          <Organization>
            <xsl:value-of select="."/>
          </Organization>
        </xsl:for-each>
        <xsl:for-each select="Company">
          <Company>
            <xsl:value-of select="."/>
          </Company>
        </xsl:for-each>
        <xsl:for-each select="Vo">
          <VeOm>
            <xsl:value-of select="."/>
          </VeOm>
        </xsl:for-each>
        <xsl:for-each select="System">
          <System>
            <xsl:value-of select="."/>
          </System>
        </xsl:for-each>
        <xsl:for-each select="DateTime">
          <DateTime>
            <xsl:value-of select="."/>
          </DateTime>
        </xsl:for-each>
        <xsl:for-each select="DocumentType">
          <DocumentType>
            <xsl:value-of select="."/>
          </DocumentType>
        </xsl:for-each>
        <xsl:for-each select="JobId">
          <JobId>
            <xsl:value-of select="."/>
          </JobId>
        </xsl:for-each>
        <xsl:for-each select="JobId">
          <JobId>
            <xsl:value-of select="."/>
          </JobId>
        </xsl:for-each>
        <xsl:for-each select="DocumentId">
          <DocumentId>
            <xsl:value-of select="."/>
          </DocumentId>
        </xsl:for-each>
        <xsl:for-each select="LevId">
          <LevId>
            <xsl:value-of select="."/>
          </LevId>
        </xsl:for-each>
        <xsl:for-each select="Intressentnummer">
          <Intressentnummer>
            <xsl:value-of select="."/>
          </Intressentnummer>
        </xsl:for-each>

        <Document>
          <xsl:for-each select="ItrID">
            <ItrID>
              <xsl:value-of select="."/>
            </ItrID>
          </xsl:for-each>
          <xsl:for-each select="KtoID">
            <KtoID>
              <xsl:value-of select="."/>
            </KtoID>
          </xsl:for-each>
          <xsl:for-each select="Edt">
            <Edt>
              <xsl:value-of select="."/>
            </Edt>
          </xsl:for-each>
        </Document>
      </Metadata>


      <Parameters>
        <xsl:for-each select="Language">
          <Language>
            <xsl:value-of select="."/>
          </Language>
        </xsl:for-each>

        <Printer>
          <xsl:for-each select="PrinterName">
            <PrinterName>
              <xsl:value-of select="."/>
            </PrinterName>
          </xsl:for-each>
          <xsl:for-each select="PrinterType">
            <PrinterType>
              <xsl:value-of select="."/>
            </PrinterType>
          </xsl:for-each>
          <xsl:for-each select="Tray">
            <Tray>
              <xsl:value-of select="."/>
            </Tray>
          </xsl:for-each>
          <xsl:for-each select="OutputBin">
            <OutputBin>
              <xsl:value-of select="."/>
            </OutputBin>
          </xsl:for-each>
          <xsl:for-each select="PrintCopies">
            <PrintCopies>
              <xsl:value-of select="."/>
            </PrintCopies>
          </xsl:for-each>
        </Printer>

        <Archive>
          <xsl:for-each select="Archive">
            <Archive>
              <xsl:value-of select="."/>
            </Archive>
          </xsl:for-each>
          <xsl:for-each select="ArchiveTime">
            <ArchiveTime>
              <xsl:value-of select="."/>
            </ArchiveTime>
          </xsl:for-each>
        </Archive>

        <File>
          <xsl:for-each select="FilePath">
            <FilePath>
              <xsl:value-of select="."/>
            </FilePath>
          </xsl:for-each>
          <xsl:for-each select="FileType">
            <FileType>
              <xsl:value-of select="."/>
            </FileType>
          </xsl:for-each>
          <xsl:for-each select="FileName">
            <FileName>
              <xsl:value-of select="."/>
            </FileName>
          </xsl:for-each>
          <xsl:for-each select="Url">
            <Url>
              <xsl:value-of select="."/>
            </Url>
          </xsl:for-each>
        </File>

        <Mail>
          <xsl:for-each select="From">
            <From>
              <xsl:value-of select="."/>
            </From>
          </xsl:for-each>
          <xsl:for-each select="To">
            <To>
              <xsl:value-of select="."/>
            </To>
          </xsl:for-each>
          <xsl:for-each select="Cc">
            <Cc>
              <xsl:value-of select="."/>
            </Cc>
          </xsl:for-each>
          <xsl:for-each select="Bcc">
            <Bcc>
              <xsl:value-of select="."/>
            </Bcc>
          </xsl:for-each>
          <xsl:for-each select="Subject">
            <Subject>
              <xsl:value-of select="."/>
            </Subject>
          </xsl:for-each>
          <xsl:for-each select="Body">
            <Body>
              <xsl:value-of select="."/>
            </Body>
          </xsl:for-each>
          <xsl:for-each select="AttachmentName">
            <AttachmentName>
              <xsl:value-of select="."/>
            </AttachmentName>
          </xsl:for-each>
        </Mail>
        <EdiMail>
          <xsl:for-each select="From">
            <From>
              <xsl:value-of select="."/>
            </From>
          </xsl:for-each>
          <xsl:for-each select="To">
            <To>
              <xsl:value-of select="."/>
            </To>
          </xsl:for-each>
          <xsl:for-each select="Cc">
            <Cc>
              <xsl:value-of select="."/>
            </Cc>
          </xsl:for-each>
          <xsl:for-each select="Bcc">
            <Bcc>
              <xsl:value-of select="."/>
            </Bcc>
          </xsl:for-each>
          <xsl:for-each select="Subject">
            <Subject>
              <xsl:value-of select="."/>
            </Subject>
          </xsl:for-each>
          <xsl:for-each select="DataOutput">
            <DataOutput>
              <xsl:value-of select="."/>
            </DataOutput>
          </xsl:for-each>
          <xsl:for-each select="AttachmentName">
            <AttachmentName>
              <xsl:value-of select="."/>
            </AttachmentName>
          </xsl:for-each>
        </EdiMail>
        <External>
          <xsl:for-each select="External">
            <External>
              <xsl:value-of select="."/>
            </External>
          </xsl:for-each>
        </External>
        <ExtendedOutput>
          <xsl:for-each select="Extended">
            <Extended>
              <xsl:value-of select="."/>
            </Extended>
          </xsl:for-each>
          <xsl:for-each select="DaysToSave">
            <DaysToSave>
              <xsl:value-of select="."/>
            </DaysToSave>
          </xsl:for-each>
          <xsl:for-each select="External">
            <External>
              <xsl:value-of select="."/>
            </External>
          </xsl:for-each>
        </ExtendedOutput>
      </Parameters>

      <ExternalAttachments>
        <xsl:for-each select="type">
          <type>
            <xsl:value-of select="."/>
          </type>
        </xsl:for-each>
        <xsl:for-each select="path">
          <path>
            <xsl:value-of select="."/>
          </path>
        </xsl:for-each>
      </ExternalAttachments>

      <DocumentData>
        <Hnd>
          <xsl:for-each select="HndDtm">
            <HndDtm>
              <xsl:value-of select="."/>
            </HndDtm>
          </xsl:for-each>
        </Hnd>
        <Handl>
          <xsl:for-each select="UsrNam">
            <UsrNam>
              <xsl:value-of select="."/>
            </UsrNam>
          </xsl:for-each>
          <xsl:for-each select="UsrSgn">
            <UsrSgn>
              <xsl:value-of select="."/>
            </UsrSgn>
          </xsl:for-each>
          <xsl:for-each select="UsrTel">
            <UsrTel>
              <xsl:value-of select="."/>
            </UsrTel>
          </xsl:for-each>
          <xsl:for-each select="UsrEml">
            <UsrEml>
              <xsl:value-of select="."/>
            </UsrEml>
          </xsl:for-each>
          <xsl:for-each select="UsrAvd">
            <UsrAvd>
              <xsl:value-of select="."/>
            </UsrAvd>
          </xsl:for-each>
          <xsl:for-each select="Edt">
            <Edt>
              <xsl:value-of select="."/>
            </Edt>
          </xsl:for-each>
        </Handl>
        <Itr>
          <xsl:for-each select="ItrID">
            <ItrID>
              <xsl:value-of select="."/>
            </ItrID>
          </xsl:for-each>
          <xsl:for-each select="Nam">
            <Nam>
              <xsl:value-of select="."/>
            </Nam>
          </xsl:for-each>
          <xsl:for-each select="ItrKatKod">
            <ItrKatKod>
              <xsl:value-of select="."/>
            </ItrKatKod>
          </xsl:for-each>
          <xsl:for-each select="PrlSkt">
            <PrlSkt>
              <xsl:value-of select="."/>
            </PrlSkt>
          </xsl:for-each>
          <xsl:for-each select="IntEdt">
            <IntEdt>
              <xsl:value-of select="."/>
            </IntEdt>
          </xsl:for-each>
          <xsl:for-each select="UpsEdt">
            <UpsEdt>
              <xsl:value-of select="."/>
            </UpsEdt>
          </xsl:for-each>
          <xsl:for-each select="UtrEdt">
            <UtrEdt>
              <xsl:value-of select="."/>
            </UtrEdt>
          </xsl:for-each>
          <xsl:for-each select="UtbDirKod">
            <UtbDirKod>
              <xsl:value-of select="."/>
            </UtbDirKod>
          </xsl:for-each>
          <xsl:for-each select="InsKtoKod">
            <InsKtoKod>
              <xsl:value-of select="."/>
            </InsKtoKod>
          </xsl:for-each>
          <xsl:for-each select="ElkDirKod">
            <ElkDirKod>
              <xsl:value-of select="."/>
            </ElkDirKod>
          </xsl:for-each>
          <xsl:for-each select="Adr1">
            <Adr1>
              <xsl:value-of select="."/>
            </Adr1>
          </xsl:for-each>
          <xsl:for-each select="Adr2">
            <Adr2>
              <xsl:value-of select="."/>
            </Adr2>
          </xsl:for-each>
          <xsl:for-each select="PstNum">
            <PstNum>
              <xsl:value-of select="."/>
            </PstNum>
          </xsl:for-each>
          <xsl:for-each select="PstAdr">
            <PstAdr>
              <xsl:value-of select="."/>
            </PstAdr>
          </xsl:for-each>
          <xsl:for-each select="Land">
            <Land>
              <xsl:value-of select="."/>
            </Land>
          </xsl:for-each>
        </Itr>
        
        <Dlg>
          <xsl:for-each select="UtbTyp">
            <UtbTyp>
              <xsl:value-of select="."/>
            </UtbTyp>
          </xsl:for-each>
        </Dlg>

        <AviseringUtdelning>
          <Ske>
            <xsl:for-each select="Typ">
              <Typ>
                <xsl:value-of select="."/>
              </Typ>
            </xsl:for-each>
            <xsl:for-each select="ItrEf">
              <ItrEf>
                <xsl:value-of select="."/>
              </ItrEf>
            </xsl:for-each>
            <xsl:for-each select="SkeID">
              <SkeID>
                <xsl:value-of select="."/>
              </SkeID>
            </xsl:for-each>
            <xsl:for-each select="Nam">
              <Nam>
                <xsl:value-of select="."/>
              </Nam>
            </xsl:for-each>
            <xsl:for-each select="VoID">
              <VoID>
                <xsl:value-of select="."/>
              </VoID>
            </xsl:for-each>
            <xsl:for-each select="Adl">
              <Adl>
                <xsl:value-of select="."/>
              </Adl>
            </xsl:for-each>
            <xsl:for-each select="RgoID">
              <RgoID>
                <xsl:value-of select="."/>
              </RgoID>
            </xsl:for-each>

            <xsl:for-each select="Utdelningsinsats">
              <Utdelningsinsats>
                <xsl:for-each select="Kontotyp">
                  <Kontotyp>
                    <xsl:value-of select="." />
                  </Kontotyp>
                </xsl:for-each>
                <xsl:for-each select="PerDatum">
                  <PerDatum>
                    <xsl:value-of select="." />
                  </PerDatum>
                </xsl:for-each>
                <xsl:for-each select="UtdelningsProcent">
                  <UtdelningsProcent>
                    <xsl:value-of select="." />
                  </UtdelningsProcent>
                </xsl:for-each>
                <xsl:for-each select="BerakningsSaldo">
                  <BerakningsSaldo>
                    <xsl:value-of select="." />
                  </BerakningsSaldo>
                </xsl:for-each>
                <xsl:for-each select="Belopp">
                  <Belopp>
                    <xsl:value-of select="." />
                  </Belopp>
                </xsl:for-each>
                <xsl:for-each select="OverfortLanekonto">
                  <OverfortLanekonto>
                    <xsl:value-of select="." />
                  </OverfortLanekonto>
                </xsl:for-each>
                <xsl:for-each select="UtbetalatBank">
                  <UtbetalatBank>
                    <xsl:value-of select="." />
                  </UtbetalatBank>
                </xsl:for-each>
                <xsl:for-each select="Forlagsnr">
                  <Forlagsnr>
                    <xsl:value-of select="." />
                  </Forlagsnr>
                </xsl:for-each>
              </Utdelningsinsats>
            </xsl:for-each>
          </Ske>

          <xsl:for-each select="TotSummaOverfortLanekonto">
            <TotSummaOverfortLanekonto>
              <xsl:value-of select="." />
            </TotSummaOverfortLanekonto>
          </xsl:for-each>

          <xsl:for-each select="TotSummaUtbetalatBank">
            <TotSummaUtbetalatBank>
              <xsl:value-of select="." />
            </TotSummaUtbetalatBank>
          </xsl:for-each>
          <Regioner>
            <Region>
              <xsl:for-each select="Namn">
                <Namn>
                  <xsl:value-of select="." />
                </Namn>
              </xsl:for-each>
              <xsl:for-each select="Adr1">
                <Adr1>
                  <xsl:value-of select="." />
                </Adr1>
              </xsl:for-each>
              <xsl:for-each select="PstNum">
                <PstNum>
                  <xsl:value-of select="." />
                </PstNum>
              </xsl:for-each>
              <xsl:for-each select="PstAdr">
                <PstAdr>
                  <xsl:value-of select="." />
                </PstAdr>
              </xsl:for-each>
            <xsl:for-each select="Epost">
                <Epost>
                  <xsl:value-of select="." />
                </Epost>
              </xsl:for-each>
              <xsl:for-each select="Telnr">
                <Telnr>
                  <xsl:value-of select="." />
                </Telnr>
              </xsl:for-each>
              <xsl:for-each select="Fax">
                <Fax>
                  <xsl:value-of select="." />
                </Fax>
              </xsl:for-each>
            </Region>
          </Regioner>
        </AviseringUtdelning>
      </DocumentData>
    </wsop>
  </xsl:template>
</xsl:stylesheet>