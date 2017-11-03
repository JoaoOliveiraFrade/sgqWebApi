using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectWebApi.Models;
using Classes;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.Mime;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using ProjectWebApi.Models.Project;

//"c:\Program Files\wkhtmltopdf\bin\wkhtmltoimage.exe" --crop-y 50 --width 1200 "http://localhost:8080/grouperConsult/reportById/20" "d:\tmp\grouper20.jpg"
//"c:\Program Files\wkhtmltopdf\bin\wkhtmltoimage.exe" --crop-y 53 --javascript-delay 10000 --encoding utf-8 "http://sgq.intranet/dist/index.html#/project/report/PRJ00006615/ENTREGA00002484" "d:\tmp\PRJ00006615.jpg"
//"c:\Program Files\wkhtmltopdf\bin\wkhtmltoimage.exe" --crop-y 53 --javascript-delay 5000 --encoding utf-8 "http://sgqhml.intranet/dist/index.html#/grouper/show/34" "d:\tmp\Alcino - Release Junho.jpg"

namespace ProjectWebApi.Controllers
{
  public class eMailController : ApiController
  {
    [HttpPost]
    [Route("SendEmail")]
    public void SendEmail(Email email)
    {
        // GERAR IMAGEM

        string outfolder = @"~/tmp/"; 
        string outfile = "report";

        string[] url = new string[] { email.url };

        string[] options = new string[] { " --crop-y 53 --quiet --javascript-delay 14000 --encoding utf-8 " };

        string imageFile = Classes.HtmlToImage.convert(outfolder, outfile, url, options);

        using (MailMessage message = new MailMessage())
        {
            //message.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            message.BodyEncoding = System.Text.Encoding.Unicode;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;

            message.From = new MailAddress(email.from);
            //message.From = new MailAddress("sgq@oi.net.br");

            message.To.Add(email.to);
            //message.To.Add("joao.frade@oi.net.br");

            //foreach (string i in email.to)
            //    if (i.ToString() != "") message.To.Add(i);

            //foreach (string i in email.cc)
            //    if (i.ToString() != "") message.CC.Add(i);

            message.CC.Add("joao.frade@oi.net.br");

            message.Subject = email.subject;
            //message.Subject = "Teste Report";

            // var inlineImage = new LinkedResource(Server.MapPath(imageFile));
            var inlineImage = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath(imageFile));
            inlineImage.ContentId = Guid.NewGuid().ToString();

            var urlReturn = url[0].Replace("report", "show");
            message.Body = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Clique <a href='" + urlReturn + "'>aqui" + "</a> para mais informações sobre o projeto.</b><br><br><a href='" + urlReturn + "'>" + string.Format(@"<img src=""cid:{0}"" /></a>", inlineImage.ContentId);

            var view = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
            view.LinkedResources.Add(inlineImage);
            message.AlternateViews.Add(view);

            // ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // AlternateView alternate = AlternateView.CreateAlternateViewFromString(email.body, mimeType);
            // message.AlternateViews.Add(alternate);


            //if (email.attachment.ContentLength > 0)
            //{
            //    string fileName = Path.GetFileName(email.attachment.FileName);
            //    message.Attachments.Add(new Attachment(email.attachment.InputStream, fileName));
            //}

            using (SmtpClient smtp = new SmtpClient("relayinterno.telemar"))
            {
                smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                //smtp.Port = 587;

                // smtp.EnableSsl = true;
                // smtp.NetworkCredential NetworkCred = new NetworkCredential(email.Email, email.Password);
                // smtp.UseDefaultCredentials = true;
                // smtp.Credentials = NetworkCred;
                try
                {
                    smtp.Send(message);
                }
                catch (SmtpException) { }
            }
      }

      if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(imageFile)))
      {
          File.Delete(System.Web.Hosting.HostingEnvironment.MapPath(imageFile));
      }

    }

    [HttpPost]
    [Route("SendEmailGrouper")]
    public void SendEmailGrouper(Email email)
    {
        // GERAR IMAGEM

        string outfolder = @"~/tmp/";
        string outfile = "grouper";

        string[] url = new string[] { email.url };

		string[] options = new string[] { " --crop-y 50 --quiet --width 1200 --javascript-delay 5000 --encoding utf-8 " };

		string imageFile = Classes.HtmlToImage.convert(outfolder, outfile, url, options);

        using (MailMessage message = new MailMessage())
        {
            message.BodyEncoding = System.Text.Encoding.Unicode;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;
            message.From = new MailAddress(email.from);
            message.To.Add(email.to);
            message.CC.Add("joao.frade@oi.net.br");
            message.Subject = email.subject;
            // var inlineImage = new LinkedResource(Server.MapPath(imageFile));
            var inlineImage = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath(imageFile));
            inlineImage.ContentId = Guid.NewGuid().ToString();

			var urlReturn = url[0].Replace("report", "show");
			// message.Body = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Clique <a href='" + urlReturn + "'>aqui" + "</a> para mais informações sobre os projetos deste agrupador.</b><br><br><a href='" + urlReturn + "'>" + string.Format(@"<img src=""cid:{0}"" /></a>", inlineImage.ContentId);

			message.Body = templateImage(urlReturn, inlineImage);

            var view = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
            view.LinkedResources.Add(inlineImage);
            message.AlternateViews.Add(view);

            // ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // AlternateView alternate = AlternateView.CreateAlternateViewFromString(email.body, mimeType);
            // message.AlternateViews.Add(alternate);


            //if (email.attachment.ContentLength > 0)
            //{
            //    string fileName = Path.GetFileName(email.attachment.FileName);
            //    message.Attachments.Add(new Attachment(email.attachment.InputStream, fileName));
            //}

            using (SmtpClient smtp = new SmtpClient("relayinterno.telemar"))
            {
                smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                //smtp.Port = 587;

                // smtp.EnableSsl = true;
                // smtp.NetworkCredential NetworkCred = new NetworkCredential(email.Email, email.Password);
                // smtp.UseDefaultCredentials = true;
                // smtp.Credentials = NetworkCred;
                try
                {
                    smtp.Send(message);
                }
                catch (SmtpException) { }
            }
        }
        if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(imageFile)))
        {
            File.Delete(System.Web.Hosting.HostingEnvironment.MapPath(imageFile));
        }
    }

	public class ReportGrouper
		{
		public Email email { get; set; }

		public Grouper grouper { get; set; }

		public IList<Project> projects { get; set; }
	}

	[HttpPost]
    [Route("SendEmailGrouperHtml")]
    public void SendEmailGrouperHtml(ReportGrouper reportGrouper)
    {
        string[] url = new string[] { reportGrouper.email.url };
        // var urlReturn = url[0].Replace("report", "show");
		var urlReturn = url[0].Replace("http://localhost:8080/grouperConsult/reportById/", "http://sgq.intranet/dist/index.html#/grouper/report/");

		// urlReturn = urlReturn.Replace("http://localhost:8080", "http://sgq.intranet/dist/index.html");
		// http://sgq.intranet/dist/index.html#/grouper/show/44
		// http://localhost:8080/grouperConsult/showById/44


		string GroupId = reportGrouper.email.url.Substring(reportGrouper.email.url.LastIndexOf("/") + 1);

		var inlineImageTop = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath(@"~\images\LogoTopEmail.png"));
		inlineImageTop.ContentId = Guid.NewGuid().ToString();

		var inlineImageBottom = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath(@"~\images\LogoBottomEmail.png"));
		inlineImageBottom.ContentId = Guid.NewGuid().ToString();

		var inlineImageGlobal = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath(@"~\images\" + reportGrouper.grouper.trafficLight + "-sm.png"));
		inlineImageGlobal.ContentId = Guid.NewGuid().ToString();

		string style = sytle();

		var ListFaroes = new List<LinkedResource>();

		string detail = "";
		foreach (var project in reportGrouper.projects) {
			var inlineImage = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath(@"~\images\" + project.trafficLight + "-xs.png"));
			inlineImage.ContentId = Guid.NewGuid().ToString();
			ListFaroes.Add(inlineImage);

			detail += details(inlineImage, project);
		}

		string template = template_(style, urlReturn, inlineImageGlobal, inlineImageTop, inlineImageBottom, reportGrouper.grouper, detail);

		var view = AlternateView.CreateAlternateViewFromString(template, null, "text/html");
		view.LinkedResources.Add(inlineImageGlobal);
		view.LinkedResources.Add(inlineImageTop);
		view.LinkedResources.Add(inlineImageBottom);
		foreach(var farol in ListFaroes) {
			view.LinkedResources.Add(farol);
		}

		using (MailMessage message = new MailMessage())
        {
            message.BodyEncoding = System.Text.Encoding.Unicode;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;
            message.From = new MailAddress(reportGrouper.email.from);
            message.To.Add(reportGrouper.email.to);
            message.CC.Add("joao.frade@oi.net.br");
            message.Subject = reportGrouper.email.subject;

			//message.Body = template;
			message.AlternateViews.Add(view);

            using (SmtpClient smtp = new SmtpClient("relayinterno.telemar"))
            {
                smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                //smtp.Port = 587;

                // smtp.EnableSsl = true;
                // smtp.NetworkCredential NetworkCred = new NetworkCredential(email.Email, email.Password);
                // smtp.UseDefaultCredentials = true;
                // smtp.Credentials = NetworkCred;
                try
                {
                    smtp.Send(message);
                }
                catch (SmtpException) { }
            }
        }
    }

	private string sytle() {
		return @"
			<style>
				* {
					margin: 0px;
					border: 0px;
					padding: 0px;
				}
				body div, p, th, td {
					font-family: 'verdana';
					font-size: 13px;
				}

				td {
					padding: 2px;
					border: solid 1px #FF4500;
					background-color: #FFECE6;
					vertical-align: text-top;
				}

				.titulo {
					text-align: center;
					font-size: 20px;
					font-weight: bold;
				}

				.texto {
					font-size: 13px;
					font-weight: bold;
				}

				.table-detail {
					width: 100%;
					margin-left: auto;
					margin-right: auto;
				}

				.farolGlobal {
					width:  5%;
					height: 5%;
				}

				.detail {
					font-size: 10px;
				}
				.detail-td {
					font-size: 10px;
					text-align: center;
					border: solid 1px #FF4500;
					background-color: #FFECE6;
				}
				.detail-th {
					background-color: #FF4500;
					font-size: 12px;
					font-weight: bold;
					color: white;
					padding: 1px;
					border: solid 1px #FF4500;
				}
			</style>
		";
	}

	private string details(LinkedResource i, Project p) {
		return $@"
		<tr>
			<td class='detail-td'>
				<img src=cid:{i.ContentId}>
			</td>

			<td class='detail-td'>
				{p.subprojectDelivery}
			</td>

			<td class='detail-td' style='text-align: left'>
				{p.name}
			</td>

			<td class='detail-td' style='text-align: left'>
				{p.UN}
			</td>

			<td class='detail-td' style='text-align: left'>
				{p.N3}
			</td>

			<td class='detail-td' style='text-align: left'>
				{p.GP}
			</td>

			<td class='detail-td'>
				{p.gap}
			</td>
			<td class='detail-td' style='text-align: left'>
				{p.informative}
			</td>
		</tr>
		";
	}

	private string template_(string style, string urlReturn, LinkedResource imagemGlobal, LinkedResource ImageTop, LinkedResource ImageBottom, Grouper grouper, string detail) { 
		return $@"
		<!doctype html>
		<html >
			<head>
				<meta charset='utf-8'>
				{style}
			</head>

			<body>
				
				<div style='text-align: center'><img src=cid:{ImageTop.ContentId}></div

				<div class='titulo'>
					COMUNICADO TI – TESTE INTEGRADO
				</div>
				<br><br>

				<div class='texto'>
					Clique <a href='{urlReturn}'>aqui</a> para mais informações sobre os projetos.
				</div>
				<br>

				<table>
					<tr>
						<!--<td style='width: 5%; background-color: #FF4500; font-size: 12px;font-weight: bold;color: white;'-->
						<td style='width: 5%'>
							<label><b>Farol:</b></label>
							<br>
							<img src=cid:{imagemGlobal.ContentId}>
						</td>

						<!--<td style='width: 50%; background-color: #FF4500; font-size: 12px;font-weight: bold;color: white;'>-->
						<td style='width: 50%'>
							<label><b>Nome:</b></label>
							<br>
							<label><b>{grouper.name}</b></label>
						</td>
					</tr>

					<tr>
						<td colspan=2>
							<label><b>Resumo Executivo do Agrupador:</b></label>
							<label>{grouper.executiveSummary}</label>

							<table class='table-detail'>
								<tr>
									<th class='detail-th'>
										Farol
									</th>                                

									<th class='detail-th'>
										Proj.
									</th>

									<th class='detail-th'>
										Nome
									</th>

									<th class='detail-th'>
										UN
									</th>

									<th class='detail-th'>
										N4
									</th>

									<th class='detail-th'>
										GP
									</th>
									<th class='detail-th'>
										GAP
									</th>
									<th class='detail-th'>
										Resumo Executivo
									</th>
								</tr>
								{detail}
							</table>
						</td>
					</tr>
				</table>
				<div class='texto'>
					<p>
						Gerência de Qualidade – Testes e Release
					</p>
					<p>
						Diretoria de Desenvolvimento de Soluções
					</p>	
					<p>
						Diretoria de Tecnologia e Redes
					</p>	
				</div>

				<div style='text-align: center'><img src=cid:{ImageBottom.ContentId}></div
			</body>
		</html>
		";
	}


	private string templateImage(string urlReturn, LinkedResource imagem) {
		return $@"
		<!doctype html>
		<html >
			<head>
				<meta charset='utf-8'>
			</head>
			<body>
				<table>
                    <tr>
                        <td>
                            <b>
                                Clique <a href='{urlReturn}'>aqui</a> para mais informações sobre os projetos deste agrupador.
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
							
                        </td>
                    </tr>
				</table>
				<img src=cid:{imagem.ContentId}>
			</body>
		</html>
		";
	}
    
//[HttpPost]
    //[Route("SaveImageByHtml")]
    //public void SaveImageByHtml(Email email)
    //{
    //  var source = @"
    //    <!DOCTYPE html>
    //    <html>
    //      <body>
    //        <p>An image from W3Schools:</p>
    //        <img 
    //          src=""http://www.w3schools.com/images/w3schools_green.jpg"" 
    //          alt=""W3Schools.com"" 
    //          width=""104"" 
    //          height=""142"">
    //      </body>
    //    </html>";
    //  StartBrowser(email.body);
    //  Console.ReadLine();
    //}

    //[HttpGet]
    //[Route("HtmlToImage")]
    //public string HtmlToImage()
    //{
    //    // string outFolder, string outFilename, string[] urls,
    //    // string[] options = null, string HtmlToImageExPath = @"c:\Program Files\wkhtmltopdf\bin\wkhtmltoimage.exe

    //}

    //private void StartBrowser(string source)
    //{
    //    var th = new Thread(() =>
    //        {
    //            var webBrowser = new WebBrowser();
    //            webBrowser.ScrollBarsEnabled = false;
    //            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
    //            webBrowser.DocumentText = source;
    //            Application.Run();
    //        });
    //    th.SetApartmentState(ApartmentState.STA);
    //    th.Start();
    //}


    //private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    //{
    //  var webBrowser = (WebBrowser)sender;
    //  using (Bitmap bitmap = new Bitmap(webBrowser.Width, webBrowser.Height))
    //  {
    //    webBrowser.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));
    //    bitmap.Save(@"d:\tmp\filename.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
    //  }
    //}
    }
}
