<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:param name="TableName"/>
  <xsl:param name="Title"/>
  <xsl:param name="Url"/>
  <xsl:param name="Type"/>
  <xsl:template match="/">
            &lt;table id="tbl<xsl:value-of select="$TableName"/>" style="margin: 2px; width: 99.6%"&gt;
    
    <!--1st pane-->   

              &lt;tr id="trWorkflowPane<xsl:value-of select="$TableName"/>" valign="top"&gt;
                &lt;td colspan="2" id="tdWf<xsl:value-of select="$TableName"/>" valign="top" style="width: 100%; height: 100%; display: none"&gt;
                  &lt;iframe id="wfFrame<xsl:value-of select="$TableName"/>" width="100%"  name="wfFrame<xsl:value-of select="$TableName"/>" src="about:blank" scrolling="no" tabindex="-1" frameborder="0"&gt;
                  &lt;/iframe&gt;
                &lt;/td&gt; 
              &lt;/tr&gt;
    
    <!--2nd Pane-->
    <xsl:if test="$Type='Master'">
              &lt;tr id="trListPane<xsl:value-of select="$TableName"/>" valign="top"&gt;
                &lt;td colspan="2" id="tdPl<xsl:value-of select="$TableName"/>" valign="top" style="width: 100%; height: 100%; display: none"&gt;
                  &lt;table cellpadding="0" cellspacing="0" style="margin-left: 2px; margin-top: 0px;width: 100%"&gt;
                    &lt;tr&gt;
                      &lt;td id="header<xsl:value-of select="$TableName"/>" style="display: block; height: 20px; width: 100%"&gt;
                          &lt;table cellpadding="0" cellspacing="0" class="headerPane" style="width: 100%"&gt;
                            &lt;tr&gt;
                              &lt;td id="PlPaneHdr_tdTitle<xsl:value-of select="$TableName"/>" style="height: 18px; width: 97%" valign="top"&gt;
                                &lt;table id="PlPaneHdr_hdrPaneTitle<xsl:value-of select="$TableName"/>" name="hdrPaneTitle" cellpadding="2" cellspacing="3"&gt;
                                  &lt;tr&gt;
                                    &lt;td class="blbl"&gt;
                                      &lt;span id="PlPaneHdr_lblTitle<xsl:value-of select="$TableName"/>" class="wlbl"&gt;
                                      &lt;/span&gt;
                                    &lt;/td&gt;
                                  &lt;/tr&gt;
                                &lt;/table&gt;
                              &lt;/td&gt;
                              &lt;td align="right" style="height: 18px; width: 3%"&gt;
                                <!--&lt;img id="dockListPane<xsl:value-of select="$TableName"/>" title="Restore" onclick="fitWindow(this);return false;" /&gt;-->
                               &lt;div class="icolnk" onmouseover="toggleStyle(this,'icolnko');" onmouseout="toggleStyle(this,'icolnk');"
                                                             style="cursor: pointer; margin-right: 5px;"&gt;
                                    &lt;i id="dockListPane<xsl:value-of select="$TableName"/>" class="icon-resize-small" onclick="fitWindow(this);return false;"
                                  title="Restore"&gt;
                                      &lt;/i&gt;
                              &lt;/div&gt;
                    &lt;/td&gt;
                            &lt;/tr&gt;
                          &lt;/table&gt;
                        &lt;/td&gt;
                      &lt;/tr&gt;
                      &lt;tr valign="top"&gt;
                        &lt;td id="tdLst<xsl:value-of select="$TableName"/>" class="listBorder"&gt;
                          &lt;iframe id="plFrame<xsl:value-of select="$TableName"/>" width="100%"  name="plFrame<xsl:value-of select="$TableName"/>" src="List.aspx?fm=1" tabindex="-1" scrolling="no" frameborder="0"&gt;
                          &lt;/iframe&gt;
                        &lt;/td&gt;
                      &lt;/tr&gt;
                    &lt;/table&gt;
                  &lt;/td&gt;
                &lt;/tr&gt;
    </xsl:if>

    <!--3rd Pane-->
    <xsl:if test="$Type='CustomMaster'">

      &lt;tr id="trCustomListPane<xsl:value-of select="$TableName"/>" valign="top"&gt;
                  &lt;td colspan="2" id="tdCl<xsl:value-of select="$TableName"/>" valign="top" style="width: 25%; display: none"&gt;
                    &lt;table cellpadding="0" cellspacing="0" style="margin: 2px; height: 150px; width: 99.6%"&gt;
                      &lt;tr&gt;
                        &lt;td id="Td2<xsl:value-of select="$TableName"/>" style="display: block; height: 20px; width: 100%" valign="top"&gt;
                          &lt;table cellpadding="0" cellspacing="0" class="headerPane" style="width: 100%"&gt;
                      &lt;tr&gt;
                        &lt;td id="clFramehdr_tdTitle<xsl:value-of select="$TableName"/>" style="height: 18px; width: 97%" valign="top"&gt;
                          &lt;table id="clFramehdr_hdrPaneTitle<xsl:value-of select="$TableName"/>" name="hdrPaneTitle<xsl:value-of select="$TableName"/>" cellpadding="1" cellspacing="1"&gt;
                          &lt;tr&gt;
                            &lt;td class="blbl"&gt;
                              &lt;span id="clFramehdr_lblTitle<xsl:value-of select="$TableName"/>" class="blbl"&gt;
                              &lt;/span&gt;
                            &lt;/td&gt;
                          &lt;/tr&gt;
                        &lt;/table&gt;
                      &lt;/td&gt;
                      &lt;td align="right" style="height: 18px; width: 3%"&gt;
                        <!--&lt;a id="dockCustomListPane<xsl:value-of select="$TableName"/>" href="#" title="Restore" onclick="fitWindow(this);return false;"&gt;
                        &lt;/a&gt;-->
                        &lt;div class="icolnk" onmouseover="toggleStyle(this,'icolnko');" onmouseout="toggleStyle(this,'icolnk');"
                                                                   style="cursor: pointer; margin-right: 5px;"&gt;
                          &lt;i id="dockCustomListPane<xsl:value-of select="$TableName"/>" class="icon-resize-small" onclick="fitWindow(this);return false;"
                    title="Restore"&gt;&lt;/i&gt;
                          &lt;/div&gt;
                      &lt;/td&gt;
                    &lt;/tr&gt;
                  &lt;/table&gt;
                &lt;/td&gt;
              &lt;/tr&gt;
              &lt;tr valign="top"&gt;
                &lt;td id="tdCLst<xsl:value-of select="$TableName"/>" class="listBorder" valign="top"&gt;
                  &lt;iframe id="clFrame<xsl:value-of select="$TableName"/>" width="100%" height="130px" name="clFrame<xsl:value-of select="$TableName"/>" src="List.aspx?fm=1" scrolling="no" frameborder="0"&gt;
                  &lt;/iframe&gt;
                &lt;/td&gt;
              &lt;/tr&gt;
            &lt;/table&gt;
           &lt;/td&gt;
          &lt;/tr&gt;
    </xsl:if>



    &lt;/table&gt;   
   
  </xsl:template>
</xsl:stylesheet>
