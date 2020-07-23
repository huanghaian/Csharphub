using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace CAPPWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public async Task Upload()
        {
            try
            {
                var boundary = MediaTypeHeaderValue.Parse(Request.ContentType).Boundary;
                var reader = new MultipartReader(boundary.Value.Trim('"'), HttpContext.Request.Body);
                var section = await reader.ReadNextSectionAsync();
                while (section != null)
                {
                    ContentDispositionHeaderValue contentDisposition;
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (contentDisposition.IsFileDisposition())
                        {
                            
                            
                        }
                        else if (contentDisposition.IsFormDisposition())
                        {
                            // Content-Disposition: form-data; name="key"
                            //
                            // value

                            // Do not limit the key name length here because the 
                            // multipart headers length limit is already in effect.
                            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                            var encoding = Encoding.UTF8;
                            using (var streamReader = new StreamReader(
                                section.Body,
                                encoding,
                                detectEncodingFromByteOrderMarks: true,
                                bufferSize: 1024,
                                leaveOpen: true))
                            {
                                // The value length limit is enforced by MultipartBodyLengthLimit
                                var value = await streamReader.ReadToEndAsync();
                              
                            }
                        }
                    }

                    // Drains any remaining section body that has not been consumed and
                    // reads the headers for the next section.
                    section = await reader.ReadNextSectionAsync();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

    }
}
