<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="invoiceno"/>
  <xsl:variable name="newline">
    <xsl:text></xsl:text>
  </xsl:variable>
  <xsl:template match="/">
    <html>
      <STYLE>
        BODY {font-family:Candara;font-size:12pt}
        .TblStd
        {
        font-size: 12pt;
        font-family: Candara;
        border-collapse: collapse;
        }
        TD
        {
        width:1000px;
        font-size:12pt
        }
        .TblData
        {
        background-color:#405B9E;
        color: white;
        font-weight:bold;
        }
      </STYLE>
      
      <body>
        Invoice No.  <xsl:value-of select="$invoiceno"/> is Rejected, please refer the Invoice EDI Tracking screen for more details.
       &lt;br&gt;
        &lt;br&gt;
        &lt;br&gt;
        <table align="left"  border="0" bordercolor="black" cellspacing="0">
          <tr>
            <td>
              <table align="center"  border="1" class="TblStd">
                <tr>
                  <td>
                    <B>Activity</B>
                  </td>
                  <td>
                    <B>Invoice No</B>
                  </td>
                  <td>
                    <B>Sent Date</B>
                  </td>
                  <td>
                    <B>Received Date</B>
                  </td>
                  <td>
                    <B>Status</B>
                  </td>
                  <td>
                    <B>Remarks</B>
                  </td>
                </tr>
                <tr>
                  <td>
                    <xsl:value-of select="AlertData/AlertGroup/DATA/ACTVTY_NAM"/>
                  </td>
                  <td>
                    <xsl:value-of select="AlertData/AlertGroup/DATA/INVC_NO"/>
                  </td>
                  <td>
                    <xsl:value-of select="AlertData/AlertGroup/DATA/SNT_DT"/>
                  </td>
                  <td>
                    <xsl:value-of select="AlertData/AlertGroup/DATA/RCVD_DT"/>
                  </td>
                  <td>
                    <xsl:value-of select="AlertData/AlertGroup/DATA/STTS"/>
                  </td>
                  <td>
                    <xsl:value-of select="AlertData/AlertGroup/DATA/RMRKS_VC"/>
                  </td>
                </tr>



              </table>
            </td>
          </tr>
        </table>
        &lt;br&gt;
        &lt;br&gt;
        &lt;br&gt;
        &lt;br&gt;
        <table align="left" class="TblStd">
          <tr>
            
          </tr>
          <tr>
            
          </tr>

          <tr>
            <td>
              It’s a system generated email.
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
