using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;
using YSFreedom.Common.Protocol;

namespace YSFreedom.Server.HttpApi.vHost.YuanshenDispatcher
{
    internal class dispatch_yuanshen : BaseController
    {
        public dispatch_yuanshen()
        {
            _handlers.Add("/query_region_list", QueryRegionList);
            _handlers.Add("/query_cur_region", QueryCurrentRegion);
        }

        private async Task QueryRegionList(IHttpContext context, Func<Task> nextHandler)
        {
            QueryRegionListHttpRsp regionList = new QueryRegionListHttpRsp
            {
                /// TODO : Figure out how to decrypt Client Config
                
                ClientSecretKey = ByteString.CopyFrom(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Http", "Dumps", "RegionListClientSecretKey.bin"))),
                ClientCustomConfigEncrypted = ByteString.CopyFrom(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Http", "Dumps", "ClientCustomConfigEncrypted.bin"))),
                EnableLoginPc = true
            };

            regionList.RegionList.Add(new RegionSimpleInfo
            {
                Name = "os_usa",
                Title = "YuanShen Freedom",
                Type = "DEV_PUBLIC",
                DispatchUrl = "https://osusadispatch.yuanshen.com/query_cur_region"
            });

            regionList.RegionList.Add(new RegionSimpleInfo
            {
                Name = "os_usa_2",
                Title = "YuanShen Freedom 2",
                Type = "DEV_PUBLIC",
                DispatchUrl = "https://osusadispatch.yuanshen.com/query_cur_region"
            });

            /// NOTE : If there's only one region, client automatically join it
            /*
            regionList.RegionList.Add(new RegionSimpleInfo
            {
                Name = "os_euro",
                Title = "Europe",
                Type = "DEV_PUBLIC",
                DispatchUrl = "http://oseurodispatch.yuanshen.com/query_cur_region"
            });

            regionList.RegionList.Add(new RegionSimpleInfo
            {
                Name = "os_asia",
                Title = "Asia",
                Type = "DEV_PUBLIC",
                DispatchUrl = "http://osasiadispatch.yuanshen.com/query_cur_region"
            });

            regionList.RegionList.Add(new RegionSimpleInfo
            {
                Name = "os_cht",
                Title = "TW, HK, MO",
                Type = "DEV_PUBLIC",
                DispatchUrl = "http://oschtdispatch.yuanshen.com/query_cur_region"
            });
            */

            context.Response = GetBase64Response(context.Request, regionList.ToByteArray());
            return;
        }

        private async Task QueryCurrentRegion(IHttpContext context, Func<Task> nextHandler)
        {
            String Version;

            context.Request.QueryString.TryGetByName("version", out Version);

            if (String.IsNullOrWhiteSpace(Version))
            {
                context.Response = GetBaseResponse(context.Request, "CAESGE5vdCBGb3VuZCB2ZXJzaW9uIGNvbmZpZw==");
                await Task.Factory.GetCompleted();
            }

            /// TODO : Make a better implementation

            QueryCurrRegionHttpRsp currRegion = new QueryCurrRegionHttpRsp
            {
                /// TODO : Figure out how to decrypt Region Config
                
                ClientSecretKey = ByteString.CopyFrom(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Http", "Dumps", "RegionClientSecretKey.bin"))),
                RegionCustomConfigEncrypted = ByteString.CopyFrom(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Http", "Dumps", "RegionCustomConfigEncrypted.bin"))),

                RegionInfo = new RegionInfo { 
                    AccountBindUrl = "https://account.mihoyo.com",
                    AreaType = "USA",
                    CdkeyUrl = "https://hk4e-api-os.mihoyo.com/common/apicdkey/api/exchangeCdkey?sign_type=2&auth_appid=apicdkey&authkey_ver=1",
                    ClientDataMd5 = "{\"fileSize\": 4401, \"remoteName\": \"data_versions\", \"md5\": \"144149abbc0bd5fd5d2a8a42dbe28c7a\"}",
                    ClientDataVersion = 2316848,
                    ClientSilenceDataMd5 = "{\"fileSize\": 514, \"remoteName\": \"data_versions\", \"md5\": \"d516b8642fb6af40caa805a337793156\"}",
                    ClientSilenceDataVersion = 2316848,
                    ClientSilenceVersionSuffix = "6b1ce6c5b2",
                    ClientVersionSuffix = "6b1ce6c5b2",
                    DataUrl = "https://autopatchhk.yuanshen.com/client_design_data/1.4_live",
                    FeedbackUrl = "https://webstatic-sea.mihoyo.com/ys/event/im-service/index.html?im_out=false&sign_type=2&auth_appid=im_ccs&authkey_ver=1&win_direction=portrait",
                    GateserverIp = "127.127.127.127",
                    GateserverPort = 22101,
                    OfficialCommunityUrl = "https://webstatic-sea.mihoyo.com/ys/event/e20200410go_community/index.html#/",
                    PayCallbackUrl = "https://osusaoaserver.yuanshen.com/recharge",
                    PrivacyPolicyUrl = "https://account.mihoyo.com/#/about/privacyInGame?app_id=4&channel_id=1&biz=hk4e_global",
                    ResourceUrl = "https://autopatchhk.yuanshen.com/client_game_res/1.4_live",
                    ResourceUrlBak = "1.4_live",

                    ResVersionConfig= new ResVersionConfig {
                        Branch = "1.4_live",
                        Md5 = "{\"remoteName\": \"res_versions_external\", \"md5\": \"6bf780879dc428622eb0cf5e4a7bd480\", \"fileSize\": 257189}\r\n{\"remoteName\": \"res_versions_medium\", \"md5\": \"ba2b214d3884085c73590351753f36f5\", \"fileSize\": 126497}\r\n{\"remoteName\": \"res_versions_streaming\", \"md5\": \"3365372cce0ff35b137f216923cce24c\", \"fileSize\": 2280}\r\n{\"remoteName\": \"release_res_versions_external\", \"md5\": \"962ccc4ed9863dd0ddc319277b0df243\", \"fileSize\": 257189}\r\n{\"remoteName\": \"release_res_versions_medium\", \"md5\": \"e2c69c4ea86bc440a5b2a2087224b235\", \"fileSize\": 126497}\r\n{\"remoteName\": \"release_res_versions_streaming\", \"md5\": \"687c9fc4517832cde8474eacdb618933\", \"fileSize\": 2280}\r\n{\"remoteName\": \"base_revision\", \"md5\": \"d41d8cd98f00b204e9800998ecf8427e\", \"fileSize\": 0}",
                        ReleaseTotalSize = "0",
                        Version = 2147343,
                        VersionSuffix = "7e09fd6db0"
                    },
                },
            };

            context.Response = GetBase64Response(context.Request, currRegion.ToByteArray());
            return;
        }
    }
}