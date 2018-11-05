using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Models.AliPay
{
    public class AlipayConfig
    {

        // 应用ID,您的APPID
        public static string app_id = "2017092208867488";

        // 支付宝网关
        public static string gatewayUrl = "https://openapi.alipay.com/gateway.do";

        // 商户私钥，您的原始格式RSA私钥
        public static string private_key = "MIIEogIBAAKCAQEAx7YcwymQjvvbORdT1FlXZPe+35ievM6pyQbGEb2cAqu89EU7dRSVJp1D+S/GF4yyMH9MmgKr3US/2LH8bVmP9y7bSf741JOcO28y+DGeX5MBWAToqUbhhUd6p2qa3fY9KhY7Vj6LjiaVJFHQgoEUcs/SejgqBHXOaq6EGarG3x9MJHRDP4W2HdgyAw4dk3Cvrre7MxFy6kBfyuPFGoBvBtrwPxhc3gJBI2kC7iv/WAGKe+gvB+FlaXxRJSwA7/P9HtUI++7Oupx+SqHRaiBqAqzdKEKTqKqQ+jfMWVbEJ8DXgP6+w3+99lq61TPyA1qZJLcqGhRBuCFPFeliDF5VEwIDAQABAoIBAGJrO+dtz2C3e4S6G0TVtnz6nqUASJIRYpi/7tKFV6H1Uowqpi9/sjUyYXIm+f9XBeVihrSLRnknzUeUBys4bPtKqyTfM29HQ0IC/eJUSZEeGBZxbNsnJDKTVD23CpUCKYhZJmrNLeci2nLootH9nL8r7bTvgr1p9Qyb7lvbzl/b5Hwm97idhSyv+OqIPuXu61kaJPF9eI/CwqplGm2i2SiAnE9SZBUXTj+R6bwUZwJ1e2g1ZkXm5S2oxsn0bt+mXZoPm21FtptHEHqJW9NqskuCXM/QOVRSivi13mquBLO2b35oIf92ZIDXkNcMFnMdU3frRQo0IYgiksfH9l6oYkECgYEA7dH3paYyQ7tOjbQfhvX+MAXNtcqel9VSFc6PCsUbIXr+07vXusMKl3cg7rH3giQUovqww4tvW/VJkdk6F4SApAqX5LLb4oxvDWPHjMC0oRLkJNsahjxRAffYKXtlojKZ1ynw00ellsMAWJ4azajgp2u3TnuUJfCAruLajeDNuSECgYEA1vpfeqOEdy3DJrfKG4QfrlqxND+JfD+CUdbrh5cy4m8EcUtbjl1dNKPi3S+oCjcVi1Ca7rKpMkNAu93cZJR51G1EUbiAqj2OsT5oxlD1OT6hiXqC31Oi4DSk751AODWRwsTOhWL51dmJwSk+Thu3dOc2boQWk/1uZYqBHmuKg7MCgYBQhAHAB2CFJekWeKEj5CzGvXBD1/GRnhtIXsJ29vyUfTvBG5uSE26GkUKJDZ+qY+TIuO8XhSGSlJzv0Aem5qlW4G9EcnmXXcxLUDjDIosE7YxoHfYA9HLIo8x/XtOt8ku7WOu73EnnnOWFGyIf9sj4ZbON0rD5l0855nvwBOghgQKBgDeUo/rhK+5kNoBcJDTZBBff+tC3XKRzBw6BsoNr5AwHB+8CvLkiCmcBuDXXjGqXs0pBnMz6BCSqnqHzynd2q8jwNympuhpJsArDR/N+Guih+MHCmvOCfCnbcolA7smZkAX7PCngXRedFrCKKUGoiNYAsWIBZJT+gPxfwuJKi5VhAoGAN4tuNo87DbnwuizWNI6GoH77oMmie0yYxkYW8QiBBN0bQ3xTzlYDdV0MQBCwsSGtq5BStpmt1Nx8w8izE5vbhjnS9DsnTnBogb6DZr5iC7z1rqS/UfAMbyFcrRlQA2L8dz83y4anV0fqNN2Wunv8beHICtpjR+ZS651rBgB/cZQ=";

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string alipay_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAx7YcwymQjvvbORdT1FlXZPe+35ievM6pyQbGEb2cAqu89EU7dRSVJp1D+S/GF4yyMH9MmgKr3US/2LH8bVmP9y7bSf741JOcO28y+DGeX5MBWAToqUbhhUd6p2qa3fY9KhY7Vj6LjiaVJFHQgoEUcs/SejgqBHXOaq6EGarG3x9MJHRDP4W2HdgyAw4dk3Cvrre7MxFy6kBfyuPFGoBvBtrwPxhc3gJBI2kC7iv/WAGKe+gvB+FlaXxRJSwA7/P9HtUI++7Oupx+SqHRaiBqAqzdKEKTqKqQ+jfMWVbEJ8DXgP6+w3+99lq61TPyA1qZJLcqGhRBuCFPFeliDF5VEwIDAQAB";

        // 签名方式
        public static string sign_type = "RSA2";

        // 编码格式
        public static string charset = "UTF-8";

    }


}