#!/bin/bash
curl -X POST -H "Authorization: key= xxxxx--put your key here ---" -H "Content-Type: application/json" \
   -d '{
  "data": {
    "notification": {
        "title": "FCM Message",
        "body": "This is an FCM Message",
        "icon": "/zu-logo.png",
		"click_action" : "https://inside.zu.ac.ae"
    }
  },
  "to": "FCM Token"
}' https://fcm.googleapis.com/fcm/send
