<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:param name="password"/>
    <xsl:param name="username"/>
    <xsl:template match="/">
        <html>
            <body>
                <FONT name="arial" COLOR="black">
                    Dear  <xsl:value-of select="$username"/>,<br></br>
                    <br></br>
                    Your password has been changed successfully. Please login using the following password. <br></br>
                    <br></br>
                    Password : <B>

                        <xsl:value-of select="$password"/>

                    </B>
                   
                </FONT>

            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>

