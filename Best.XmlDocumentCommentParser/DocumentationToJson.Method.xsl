<?xml version="1.0" encoding="iso-8859-1" ?>


<!-- ================================================================================ -->
<!-- Amend, distribute, spindle and mutilate as desired, but don't remove this header -->
<!-- A simple XML Documentation to basic HTML transformation stylesheet -->
<!-- (c)2005 by Emma Burrows -->
<!-- ================================================================================ -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <xsl:apply-templates select="//member[contains(@name,'M:')]"/>
  </xsl:template>
  
  <!-- METHOD TEMPLATE -->
  <xsl:template match="//member[contains(@name,'M:')]">

    <!-- Two variables to make code easier to read -->
    <!-- A variable for the name of this type -->
    <xsl:variable name="MemberName"
                    select="substring-after(@name, '.')"/>

    <!-- Get the type's fully qualified name without the T: prefix -->
    <xsl:variable name="FullMemberName"
                    select="substring-after(@name, ':')"/>

    <!-- If this is a constructor, display the type name 
              (instead of "#ctor"), or display the method name -->
    <xsl:variable name="MethodMemberName">
      <xsl:choose>
        <xsl:when test="contains(@name, '#ctor')">
          <xsl:value-of select="$MemberName"/>
          <xsl:value-of select="substring-after(@name, '#ctor')"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="substring-after(@name, '.')"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>

    <xsl:variable name="MethodFullMemberName">
      <xsl:choose>
        <xsl:when test="contains(@name, '#ctor')">
          <xsl:value-of select="$FullMemberName"/>
          <xsl:value-of select="substring-after(@name, '#ctor')"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="substring-after(@name, ':')"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    
    {
    "Name":"<xsl:value-of select="$MethodMemberName"/>",
    "FullName":"<xsl:value-of select="$MethodFullMemberName"/>",

    <xsl:if test="count(summary)!=0">
      "Summary":"<xsl:apply-templates select="summary"/>",
    </xsl:if>

    <xsl:if test="count(code)!=0">
      "Code:":"<xsl:apply-templates select="code"/>",
    </xsl:if>

    <xsl:if test="count(exception)!=0">
      "Exceptions":"<xsl:apply-templates select="exception"/>",
    </xsl:if>

    <xsl:if test="count(example)!=0">
      "Examples:":"<xsl:apply-templates select="example"/>",
    </xsl:if>

    <xsl:if test="count(remarks)!=0">
      "Remarks:":"<xsl:apply-templates select="remarks"/>",
    </xsl:if>

    <xsl:if test="count(returns)!=0">
      "Returns":"<xsl:apply-templates select="returns"/>",
    </xsl:if>
    
    <xsl:if test="count(param)!=0">
      "Parameters":
      [
      <xsl:for-each select="param">
        {
          "ParamName":"<xsl:value-of select="@name"/>",
          "ParamDescription":"<xsl:apply-templates />"
        },
      </xsl:for-each>
      ],
    </xsl:if>

    }
  </xsl:template>

  <!-- OTHER TEMPLATES -->
  <!-- Templates for other tags -->
  <xsl:template match="c">
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="code">
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="example">
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="exception">
    <xsl:value-of select="substring-after(@cref,'T:')"/>:
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="include">
    <A HREF="{@file}">External file</A>
  </xsl:template>

  <xsl:template match="para">
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="param">
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="paramref">
    <xsl:value-of select="@name" />
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="permission">
    <P>
      <STRONG>Permission: </STRONG>
      <EM>
        <xsl:value-of select="@cref" />
      </EM>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="remarks">
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="returns">
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="see">
    See: <xsl:value-of select="substring-after(@cref,'T:')" />
  </xsl:template>

  <xsl:template match="seealso">
    See also: <xsl:value-of select="substring-after(@cref,'T:')" />
  </xsl:template>

  <xsl:template match="summary">
    <xsl:call-template name="escapeJson">
    </xsl:call-template>
  </xsl:template>
  
  <xsl:template match="list">
    <xsl:choose>
      <xsl:when test="@type='bullet'">
        <UL>
          <xsl:for-each select="listheader">
            <LI>
              <strong>
                <xsl:value-of select="term"/>:
              </strong>
              <xsl:value-of select="definition"/>
            </LI>
          </xsl:for-each>
          <xsl:for-each select="list">
            <LI>
              <strong>
                <xsl:value-of select="term"/>:
              </strong>
              <xsl:value-of select="definition"/>
            </LI>
          </xsl:for-each>
        </UL>
      </xsl:when>
      <xsl:when test="@type='number'">
        <OL>
          <xsl:for-each select="listheader">
            <LI>
              <strong>
                <xsl:value-of select="term"/>:
              </strong>
              <xsl:value-of select="definition"/>
            </LI>
          </xsl:for-each>
          <xsl:for-each select="list">
            <LI>
              <strong>
                <xsl:value-of select="term"/>:
              </strong>
              <xsl:value-of select="definition"/>
            </LI>
          </xsl:for-each>
        </OL>
      </xsl:when>
      <xsl:when test="@type='table'">
        <TABLE>
          <xsl:for-each select="listheader">
            <TH>
              <TD>
                <xsl:value-of select="term"/>
              </TD>
              <TD>
                <xsl:value-of select="definition"/>
              </TD>
            </TH>
          </xsl:for-each>
          <xsl:for-each select="list">
            <TR>
              <TD>
                <strong>
                  <xsl:value-of select="term"/>:
                </strong>
              </TD>
              <TD>
                <xsl:value-of select="definition"/>
              </TD>
            </TR>
          </xsl:for-each>
        </TABLE>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <!-- TEMPLATE for data process -->
  <xsl:template name="escapeJson">
    <xsl:param name="pText" select="."/>

    <xsl:variable name="outputEscapeBackSlash">
      <xsl:call-template name="escapeBackSlash">
        <xsl:with-param name="pText" select="$pText"/>
      </xsl:call-template>
    </xsl:variable>

    <xsl:variable name="outputEscapeQuote">
      <xsl:call-template name="escapeQuote">
        <xsl:with-param name="pText" select="$outputEscapeBackSlash"/>
      </xsl:call-template>
    </xsl:variable>

    <xsl:copy-of select="$outputEscapeQuote"/>

  </xsl:template>

  <xsl:template name="escapeQuote">
    <xsl:param name="pText"/>
    <xsl:param name="pResult"/>

    <xsl:if test="string-length($pText) >0">
      <xsl:value-of select=
        "substring-before(concat($pText, '&quot;'), '&quot;')"/>

      <xsl:if test="contains($pText, '&quot;')">
        <xsl:text>\"</xsl:text>

        <xsl:call-template name="escapeQuote">
          <xsl:with-param name="pText" select=
          "substring-after($pText, '&quot;')"/>
        </xsl:call-template>
      </xsl:if>
    </xsl:if>
  </xsl:template>

  <xsl:template name="escapeBackSlash">
    <xsl:param name="pText"/>

    <xsl:if test="string-length($pText) >0">
      <xsl:value-of select=
        "substring-before(concat($pText, '\'), '\')"/>

      <xsl:if test="contains($pText, '\')">
        <xsl:text>\\</xsl:text>

        <xsl:call-template name="escapeBackSlash">
          <xsl:with-param name="pText" select=
          "substring-after($pText, '\')"/>
        </xsl:call-template>
      </xsl:if>
    </xsl:if>
  </xsl:template>

  <xsl:template name="replace-string">
    <xsl:param name="text"/>
    <xsl:param name="replace"/>
    <xsl:param name="with"/>
    <xsl:choose>
      <xsl:when test="contains($text,$replace)">
        <xsl:value-of select="substring-before($text,$replace)"/>
        <xsl:value-of select="$with"/>
        <xsl:call-template name="replace-string">
          <xsl:with-param name="text"
  select="substring-after($text,$replace)"/>
          <xsl:with-param name="replace" select="$replace"/>
          <xsl:with-param name="with" select="$with"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>

