﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using RemoteFork.Plugins;
using RemoteFork.Settings;

namespace RemoteFork.Requestes {
    public class UserUrlsRequestHandler : BaseRequestHandler<string> {
        public const string URL_PATH = "userurls";
        public const string PARAM_URLS = "urls.m3u";

        public override string Handle(HttpRequest request, HttpResponse response) {
            var result = new List<Item>();

            if ((ProgramSettings.Settings.UserUrls != null) && (ProgramSettings.Settings.UserUrls.Length > 0)) {
                result.AddRange(from string url in ProgramSettings.Settings.UserUrls
                    select new Item {
                        Name = url.Split('\\').Last().Split('/').Last(),
                        Link = url,
                        Type = ItemType.FILE
                    });
            }

            response.ContentType = MimeTypes.Get(PARAM_URLS.Substring(PARAM_URLS.IndexOf('.')));

            return ResponseSerializer.ToM3U(result.ToArray());
        }
    }
}
