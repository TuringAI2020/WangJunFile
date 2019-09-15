using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WangJun.Yun;

namespace WangJun.API
{
    /// <summary>
    /// FileServiceAPI 的摘要说明
    /// </summary>
    public class FileServiceAPI : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = CONST.application_json;
                //var inputParam = HttpRequestParam.Parse(context);
                var path = context.Request.QueryString["path"];
                var fileName = context.Request.QueryString["fileName"];
                byte[] buffer = new byte[(int)context.Request.Files[0].InputStream.Length];
                context.Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);
                var res = FileService.GetInst(context.Server.MapPath("~")).SaveTo(path, fileName, buffer);
                context.Response.Write(res.ToJson());
            }
            catch (Exception ex)
            {
                
                context.Response.Write(RES.FAIL($"{ex.Message}\t{((null != ex.InnerException) ? ex.InnerException.Message : string.Empty)}").ToJson());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}