namespace ContosoAcai.Infrastructure.Email;

public static class Constants
{
    public static string EmailBody => """
                                    <!DOCTYPE html>
                                    <html>
                                    <head>
                                        <!-- Compiled with Bootstrap Email version: 1.3.1 -->
                                        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
                                        <meta http-equiv="x-ua-compatible" content="ie=edge" />
                                        <meta name="x-apple-disable-message-reformatting" />
                                        <meta name="viewport" content="width=device-width, initial-scale=1" />
                                        <meta name="format-detection" content="telephone=no, date=no, address=no, email=no" />
                                        <style type="text/css">
                                            body, table, td {
                                                font-family: Helvetica,Arial,sans-serif !important;
                                            }
                                            .ExternalClass {
                                                width: 100%;
                                            }
                                            .ExternalClass, .ExternalClass p, .ExternalClass span,
                                            .ExternalClass font, .ExternalClass td, .ExternalClass div {
                                                line-height: 150%;
                                            }
                                            a {
                                                text-decoration: none;
                                                color: inherit;
                                            }
                                            * {
                                                color: inherit;
                                            }
                                            a[x-apple-data-detectors], u + #body a, #MessageViewBody a {
                                                color: inherit !important;
                                                text-decoration: none !important;
                                                font-size: inherit !important;
                                                font-family: inherit !important;
                                                font-weight: inherit !important;
                                                line-height: inherit !important;
                                            }
                                            img {
                                                -ms-interpolation-mode: bicubic;
                                            }
                                            table:not([class^=s-]) {
                                                font-family: Helvetica,Arial,sans-serif;
                                                mso-table-lspace: 0pt;
                                                mso-table-rspace: 0pt;
                                                border-spacing: 0;
                                                border-collapse: collapse;
                                            }
                                            table:not([class^=s-]) td {
                                                border-spacing: 0;
                                                border-collapse: collapse;
                                            }
                                            .centered-button {
                                                background-color: #ffffff;
                                                color: #ffffff;
                                                border: none;
                                                padding: 10px 20px;
                                                font-size: 16px;
                                                cursor: pointer;
                                                text-decoration: none;
                                                display: inline-block;
                                            }
                                            .centered-button:hover {
                                                background-color: #4c218d;
                                            }
                                            .btn {
                                                display: block;
                                                background-color: #6c2b6d;
                                                color: white;
                                                border: none;
                                                border-radius: 20px;
                                                padding: 10px 20px;
                                                font-size: 16px;
                                                cursor: pointer;
                                                box-shadow: 0 2px 4px rgba(0,0,0,0.2);
                                                transition: background-color 0.3s;
                                                text-align: center;
                                                text-decoration: none;
                                            }
                                            @media screen and (max-width: 600px) {
                                                .w-full, .w-full > tbody > tr > td {
                                                    width: 100% !important;
                                                }
                                                .w-24, .w-24 > tbody > tr > td {
                                                    width: 96px !important;
                                                }
                                                .w-40, .w-40 > tbody > tr > td {
                                                    width: 160px !important;
                                                }
                                                .p-lg-10:not(table), .p-lg-10:not(.btn) > tbody > tr > td, .p-lg-10.btn td a {
                                                    padding: 0 !important;
                                                }
                                                .p-3:not(table), .p-3:not(.btn) > tbody > tr > td, .p-3.btn td a {
                                                    padding: 12px !important;
                                                }
                                                .p-6:not(table), .p-6:not(.btn) > tbody > tr > td, .p-6.btn td a {
                                                    padding: 24px !important;
                                                }
                                                *[class*=s-lg-] > tbody > tr > td {
                                                    font-size: 0 !important;
                                                    line-height: 0 !important;
                                                    height: 0 !important;
                                                }
                                                .s-4 > tbody > tr > td {
                                                    font-size: 16px !important;
                                                    line-height: 16px !important;
                                                    height: 16px !important;
                                                }
                                                .s-6 > tbody > tr > td {
                                                    font-size: 24px !important;
                                                    line-height: 24px !important;
                                                    height: 24px !important;
                                                }
                                                .s-10 > tbody > tr > td {
                                                    font-size: 40px !important;
                                                    line-height: 40px !important;
                                                    height: 40px !important;
                                                }
                                            }
                                        </style>
                                    </head>
                                    <body class="bg-light" style="outline:0; width:100%; min-width:100%; height:100%; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%; font-family:Helvetica,Arial,sans-serif; line-height:24px; font-weight:normal; font-size:16px; box-sizing:border-box; color:#000; margin:0; padding:0;" bgcolor="#6c2b6d">
                                        <table class="bg-light body" role="presentation" border="0" cellpadding="0" cellspacing="0" style="width:100%; height:100%; margin:0; padding:0;" bgcolor="#6c2b6d">
                                            <tr>
                                                <td valign="top" align="left" style="padding:0 16px;">
                                                    <table class="container" role="presentation" border="0" cellpadding="0" cellspacing="0" style="width:100%; max-width:600px; margin:0 auto;">
                                                        <tr>
                                                            <td align="left">
                                                                <!-- top spacing -->
                                                                <div style="height:40px; font-size:40px; line-height:40px;">&nbsp;</div>

                                                                <!-- logo -->
                                                                <div style="text-align:center; margin:0 auto;">
                                                                    <img class="w-24" src="https://stcontosoacaiweastus3.blob.core.windows.net/logo/logoacai 1.svg" alt="Logo" style="max-height:150px; display:block; margin:0 auto;" />
                                                                </div>

                                                                <!-- spacing -->
                                                                <div style="height:40px; font-size:40px; line-height:40px;">&nbsp;</div>

                                                                <!-- card -->
                                                                <table class="card p-6 p-lg-10" role="presentation" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff" style="width:100%; border:1px solid #e2e8f0; border-radius:6px; overflow:hidden;">
                                                                    <tr>
                                                                        <td style="padding:40px;">
                                                                         {body}
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                                <!-- bottom spacing -->
                                                                <div style="height:40px; font-size:40px; line-height:40px;">&nbsp;</div>

                                                                <!-- footer -->
                                                                <div style="text-align:center; color:#718096; font-size:14px; line-height:20px; color:white">
                                                                    Enviado por Contoso Açaí
                                                                </div>

                                                                <div style="height:24px; font-size:24px; line-height:24px;">&nbsp;</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </body>
                                    </html>

                                    """;
}