- Software Requirements to test
1. Postman etc. to post requests

- Restful methods

There are three methods that are implemented:

1. http://localhost:59548/api/notifications POST

This method is used to subscribe to notifications. A json request in following structure is posted to the url:
{
"username": "bbcUser2",
"accessToken": "o.XFPIUl0Q4MChltHv0nPYetAbCMuizFOm"
}
In response the following data is received on success:
{"username":"bbcUser2","accessToken":"o.XFPIUl0Q4MChltHv0nPYetAbCMuizFOm","creationTime":"2018-01-28T19:30:55.109032+00:00","numOfNotificationsPushed":0}

In case of duplicate subscription i.e. a duplicate username the following response is received:
{"status":"Failed","message":"Failed to add subscription"}

2. http://localhost:59548/api/notifications GET

This method returns a list of all the subscriptions. The response is as follows:
[
    {
        "username": "bbcUser2",
        "accessToken": "o.XFPIUl0Q4MChltHv0nPYetAbCMuizFOm",
        "creationTime": "2018-01-28T18:56:13.5822129+00:00",
        "numOfNotificationsPushed": 0
    },
    {
        "username": "bbcUser",
        "accessToken": "o.XFPIUl0Q4MChltHv0nPYetAbCMuizFO",
        "creationTime": "2018-01-28T18:56:21.4217449+00:00",
        "numOfNotificationsPushed": 0
    }
]

3. http://localhost:59548/api/notifications PUT

This method is used to send out a notification to a particular user. The request looks like:
{
	"username":"bbcUser2",
	"title":"Test Title",
	"text":"Test Text"
}
The success response looks like:
{"isSent":true,"status":"Success"}
The failure response looks like:
{"isSent":false,"status":"Unable to send notification"}