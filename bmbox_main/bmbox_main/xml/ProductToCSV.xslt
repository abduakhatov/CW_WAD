<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="text" />
  <xsl:template match="/">
<!-- Labels(Titles) for elements-->
    <xsl:text>Id, </xsl:text>
    <xsl:text>Name, </xsl:text>
    <xsl:text>Brand, </xsl:text>
    <xsl:text>Type</xsl:text>
    <xsl:text>Cost,</xsl:text>
    <xsl:text>QuantityLeft</xsl:text>
    <xsl:text>&#xa;</xsl:text>

    <!-- All elements printed in a loop -->
    <xsl:for-each select="/Products/Product">
      <!-- this is for attribute @ sign-->
      <xsl:value-of select="@Id"/>
      <xsl:text>, </xsl:text>
      <xsl:value-of select="Name"/>
      <xsl:text>, </xsl:text>
      <xsl:value-of select="Brand"/>
      <xsl:text>, </xsl:text>
      <xsl:value-of select="Type"/>
      <xsl:text>, </xsl:text>
      <xsl:value-of select="Cost"/>
      <xsl:text>, </xsl:text>
      <xsl:value-of select="QuantityLeft"/>
      <xsl:text>&#xa;</xsl:text>
    </xsl:for-each>

  </xsl:template>
</xsl:stylesheet>
