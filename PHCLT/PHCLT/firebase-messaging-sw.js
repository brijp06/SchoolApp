importScripts('https://www.gstatic.com/firebasejs/10.13.2/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/10.13.2/firebase-messaging-compat.js');

firebase.initializeApp({
    apiKey: "AIzaSyA826U07wLYb7kx-koMP_u9uwuajHm9Ctw",
    authDomain: "chankyaschool-6f741.firebaseapp.com",
    projectId: "chankyaschool-6f741",
    storageBucket: "chankyaschool-6f741.firebasestorage.app",
    messagingSenderId: "189207919351",
    appId: "1:189207919351:web:5bbb27844612c0ccfb31f2",
    measurementId: "G-LXYMYBD7TF"
});

// Retrieve an instance of Firebase Messaging so that it can handle background
// messages.
const messaging = firebase.messaging();


messaging.onBackgroundMessage((payload) => {
    console.log(
        '[firebase-messaging-sw.js] Received background message ',
        payload
    );
    //// Customize notification here
    //const notificationTitle = 'Background Message Title';
    //const notificationOptions = {
    //    body: 'Background Message body.',
    //    icon: '/firebase-logo.png'
    //};

    //self.registration.showNotification(notificationTitle, notificationOptions);
});

