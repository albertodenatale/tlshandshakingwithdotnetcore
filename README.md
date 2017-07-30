# tlshandshakingwithdotnetcore
A repository containing a client and a server working together at TCP level to establish a secure TLS channel.

Useful links:

https://blogs.msdn.microsoft.com/kaushal/2013/08/02/ssl-handshake-and-https-bindings-on-iis/
http://www.c-sharpcorner.com/article/building-a-tcp-server-in-net-core-on-ubuntu/
https://msdn.microsoft.com/en-us/library/system.net.security.sslstream(v=vs.110).aspx
http://www.codeguru.com/columns/dotnet/using-secure-sockets-in-.net.html
https://msdn.microsoft.com/en-us/library/system.net.security.sslstream(v=vs.110).aspx

Userful terminal commands:

start handshaking with the server: openssl s_client -tls1 -connect 127.0.0.1:5678 -msg
creates self signed certificate: openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 365
Convert PEM & Private Key to PFX/P12: openssl pkcs12 -export -out cert.pfx -inkey key.pem -in cert.pem
