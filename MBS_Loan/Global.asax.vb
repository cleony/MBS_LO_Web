
Imports Mixpro.MBSLibary
Imports System.Globalization
Imports System.Threading


Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        '======== ใส่ key stimulsoft 
        Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHnfitI9oGUCSBIUK1nozLCxqGXREmILqnfHJHsCnoj6X+6Ul3" +
"yg0x3kaeGzoKQFw/QaXxnckI//r4iULR5W8Om5Jz2i7h0qG7YJXHBMXnD9u/7SfFJ1unFEuqeZi2eNQobA5TuC9AM0" +
"BrYkbUAtrHN2nwANO35vBJEGrFXnrw2o1vHe0/lm/b+UahD6BBxLxT15wPYF/MF6jnMFlXYy5cod9hnPSF2JWvkY70" +
"ttNwI7nazg19imtF6N1Gx4IJjUFZGxF95cc9NrfdPxs7i8mLWfZDWn4AVeZ50J5k9tuGtcZQRgRXm1XYX/PY0QeWkX" +
"YtufXrZlSNbaIRVSrIkvanA+56uppCksojzCZJPggTfHVIMSzqklCitKW67hugVSWlz+Df/0o8q5XwqkK6DUQPGIDI" +
"xN837Ui4PTr+MUMjWw5hKzrBNjj1bdofzBrp4+vzaSPLe01cMgLh6CmPYN6e3atVSAOBe1OEsgHu6eqRDloMpePRE5" +
"7KFZidrwxEdX0SxHVtF86C7LKxch3LPd1lpcZ0ntIQW2bsC9HZBkG2GEUg=="

    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)

        Share.DbConnect = Constant.DBConnection.SqlServer

        Share.DatabaseInfo.DataBaseName = Share.FormatString(ConfigurationManager.AppSettings("DataBaseName"))
        Share.DatabaseInfo.ServerName = Share.FormatString(ConfigurationManager.AppSettings("ServerName"))
        Share.DatabaseInfo.UserName = Share.FormatString(ConfigurationManager.AppSettings("UserName"))
        Share.DatabaseInfo.PassWord = Share.FormatString(ConfigurationManager.AppSettings("PassWord"))

        ''====== เช็ค ip =================
        Dim CheckIp As String = Share.FormatString(ConfigurationManager.AppSettings("CheckIp"))
        If CheckIp.ToLower = "on" Then
            Dim ip As String = ""
            ip = GetIpAddress()
            If Not (SQLData.Table.IsDuplicateID("WEB_AllowIP", "IPAddress", ip)) Then

                Response.Redirect("~/closepage2.html")
                Exit Sub
            End If
        End If

        Dim SetTime As String = Share.FormatString(ConfigurationManager.AppSettings("SetTime"))
        If SetTime.ToLower = "on" Then
            Dim GetOpenTime As String = Share.FormatString(ConfigurationManager.AppSettings("OpenTime"))
            Dim GetCloseTime As String = Share.FormatString(ConfigurationManager.AppSettings("CloseTime"))
            Dim CloseHoliday As String = Share.FormatString(ConfigurationManager.AppSettings("CloseHoliday"))
            If CloseHoliday.ToLower = "on" Then
                If Date.Today.DayOfWeek = DayOfWeek.Saturday OrElse Date.Today.DayOfWeek = DayOfWeek.Sunday Then
                    Response.Redirect("~/closepage.html")
                    Exit Sub
                End If
            End If
            If Not (Convert.ToDateTime(GetOpenTime) <= Date.Now AndAlso Date.Now <= Convert.ToDateTime(GetCloseTime)) Then
                Response.Redirect("~/closepage.html")
                Exit Sub
            End If
        End If

        Dim newCulture As CultureInfo = DirectCast(CultureInfo.GetCultureInfo("th-TH").Clone(), CultureInfo)
        newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        newCulture.DateTimeFormat.DateSeparator = "/"
        Thread.CurrentThread.CurrentCulture = newCulture

        '========== 


    End Sub
    Private Function GetIpAddress() As String
        Dim IpAddress As String = ""
        Try
            Dim request = HttpContext.Current.Request
            ' Look for a proxy address first
            IpAddress = request.ServerVariables("HTTP_X_FORWARDED_FOR")

            ' If there is no proxy, get the standard remote address
            If String.IsNullOrWhiteSpace(IpAddress) OrElse String.Equals(IpAddress, "unknown", StringComparison.OrdinalIgnoreCase) Then
                IpAddress = request.ServerVariables("REMOTE_ADDR")
            Else
                'extract first IP
                Dim index = IpAddress.IndexOf(","c)
                If index > 0 Then
                    IpAddress = IpAddress.Substring(0, index)
                End If

                'remove port
                index = IpAddress.IndexOf(":"c)
                If index > 0 Then
                    IpAddress = IpAddress.Substring(0, index)
                End If
            End If


        Catch ex As Exception

        End Try
        Return IpAddress
    End Function

End Class