using YSFreedom.Server.HttpApi.vHost.hk4eSdk.Models;
using YSFreedom.Server.Auth;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;
using Serilog;
using Newtonsoft.Json;

namespace YSFreedom.Server.HttpApi.vHost.hk4eSdk
{
    class hk4eSdk : BaseController
    {
        private AuthService authService;

        public hk4eSdk(AuthService authSvc)
        {
            authService = authSvc;

            _handlers.Add("/hk4e_global/mdk/agreement/api/getAgreementInfos", GetAgreementInfos);
            _handlers.Add("/hk4e_global/mdk/shield/api/login", Login);
            _handlers.Add("/hk4e_global/combo/granter/api/compareProtocolVersion", CompareProtocolVersion);
            _handlers.Add("/hk4e_global/combo/granter/api/getConfig", GetConfigResponse);
            _handlers.Add("/hk4e_global/combo/granter/login/v2/login", LoginGranter);
        }
        private async Task Login(IHttpContext context, Func<Task> nextHandler)
        {
            var loginRaw = context.Request.Post.Raw;
            var loginReq = JsonConvert.DeserializeObject<LoginRequest>(Encoding.UTF8.GetString(loginRaw));

            try
            {
                var account = await authService.Login(loginReq.account, null);

                LoginResponse model = new LoginResponse
                {
                    data = new LoginResponse.Data
                    {
                        account = new LoginResponse.Data.Account
                        {
                            area_code = "**",
                            country = account.CountryCode,
                            email = account.Email,
                            is_email_verify = "1",
                            token = "PqzjI4Q7iV7cJS6NTr5jabq65DeYQ1vc",
                            uid = account.UID.ToString(),
                        },
                        device_grant_required = false,
                        reactivate_required = false,
                        realperson_required = false,
                        safe_mobile_required = false,
                    },
                    message = "OK",
                    retcode = 0
                };

                context.Response = GetJsonResponse(context.Request, model);
            }
            catch (KeyNotFoundException)
            {
                LoginResponse model = new LoginResponse
                {
                    message = "That account doesn't exist.",
                    retcode = 1 // IDK
                };

                context.Response = GetJsonResponse(context.Request, model);
            }
            catch (InvalidCredentialException)
            {
                LoginResponse model = new LoginResponse
                {
                    message = "Invalid password.",
                    retcode = 2
                };

                context.Response = GetJsonResponse(context.Request, model);
            }
            return;

        }
        private async Task LoginGranter(IHttpContext context, Func<Task> nextHandler)
        {
            var loginGranterRaw = context.Request.Post.Raw;
            //Log.Debug(Encoding.UTF8.GetString(loginGranterRaw));
            Dictionary<string,object> loginGranterReqOuter = JsonConvert.DeserializeObject<Dictionary<string,object>>(Encoding.UTF8.GetString(loginGranterRaw));
            var loginGranterReq = JsonConvert.DeserializeObject<LoginGranterRequestInner>((string)loginGranterReqOuter["data"]);

            LoginGranterResponse model = new LoginGranterResponse
            {
                data = new LoginGranterResponse.Data
                {
                    account_type = 1,
                    combo_id = "77732323",
                    combo_token = "9065ad8507d5a1991cb6fddacac5999b780bbd92",
                    data = "{\"guest\":false}",
                    heartbeat = false,
                    open_id = loginGranterReq.uid,
                },
                message = "OK",
                retcode = 0
            };

            context.Response = GetJsonResponse(context.Request, model);
            return;
        }
        private async Task GetAgreementInfos(IHttpContext context, Func<Task> nextHandler)
        {
            AgreementResponse model = new AgreementResponse
            {
                data = new AgreementResponse.Data
                {
                    marketing_agreements = new object[0]
                },
                message = "OK",
                retcode = 0,
            };

            context.Response = GetJsonResponse(context.Request, model);
            return;
        }
        private async Task CompareProtocolVersion(IHttpContext context, Func<Task> nextHandler)
        {
            CompareProtocolVersionResponse model = new CompareProtocolVersionResponse
            {
                data = new CompareProtocolVersionResponse.Data
                {
                    modified = true,
                    protocol = new CompareProtocolVersionResponse.Data.Protocol
                    {
                        app_id = 4,
                        create_time = "0",
                        id = 18,
                        language = "en",
                        major = 3,
                        minimum = 0,
                    }
                },
                message = "OK",
                retcode = 0
            };

            context.Response = GetJsonResponse(context.Request, model);
            return;
        }
        private async Task GetConfigResponse(IHttpContext context, Func<Task> nextHandler)
        {
            GetConfigResponse model = new GetConfigResponse
            {
                data = new GetConfigResponse.Data
                {
                    announce_url = "https://webstatic-sea.mihoyo.com/hk4e/announcement/index.html?sdk_presentation_style=fullscreen&sdk_screen_transparent=true&game_biz=hk4e_global&auth_appid=announcement&game=hk4e#/",
                    disable_ysdk_guard = false,
                    enable_announce_pic_popup = true,
                    log_level = "INFO", // DEBUG ?
                    protocol = true,
                    push_alias_type = 2,
                    qr_enabled = false
                },
                message = "OK",
                retcode = 0
            };

            context.Response = GetJsonResponse(context.Request, model);
            return;
        }
    }
}
