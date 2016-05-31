using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SKOffice.domain.order;
using SKOffice;
using WcfService.domain.data;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Net;
using System.Web.Hosting;
using System.ServiceModel.Web;

namespace WcfService
{
    public class RestService : IRestService
    {
        public RestService()
        {
            FileServer fs = FileServer.Instance;
            fs.startServer(9000);
        }

        OrderConfirmation IRestService.getOrder(string fileName)
        {
            Console.WriteLine("Got RESTFul connection.");
            return OrderParser.Instance.readOrder("C:\\School\\SKøkken\\" + fileName + ".e02");
        }

        public Stream DownloadFile(string fileName, string fileExtension)
        {
            string downloadFilePath = Path.Combine(HostingEnvironment.MapPath("~/FileServer/" + fileName + "/"), fileName + "." + fileExtension);

            //Write logic to create the file
            File.Create(downloadFilePath);

            String headerInfo = "attachment; filename=" + fileName + "." + fileExtension;
            WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";

            return File.OpenRead(downloadFilePath);
        }

        public void UploadFile(string fileName, Stream stream)
        {
            string FilePath = Path.Combine(HostingEnvironment.MapPath("~/FileServer/" + fileName.Split('.')[0] + "/"), fileName);

            int length = 0;
            using (FileStream writer = new FileStream(FilePath, FileMode.Create))
            {
                int readCount;
                var buffer = new byte[8192];
                while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    writer.Write(buffer, 0, readCount);
                    length += readCount;
                }
            }
        }
    }
}
