// const isloggedin = localStorage.getItem('token')
// if(!isloggedin){
//     Toastify({
//         text: "Hey User! You are already Not logged In, Redirecting...",
//         style: {
//             background: "linear-gradient(to right, #00b09b, #96c93d)",
//         },
//         callback: function() {
//             window.location.href = 'Login.html'; // Redirect after toast disappears
//         }
//     }).showToast();
// }



document.addEventListener('DOMContentLoaded', async function() {
    await NotificationInfo1()
    var n = document.getElementById('notifi')
    n.style.removeProperty('color');   
})


async function NotificationInfo1() {
    console.log('hi')
    const spinnerEl = document.querySelector('.spinnerborderindex');
    spinnerEl.style.display = 'flex';
    await fetch('https://localhost:7186/api/User/UserNotifications', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer '+localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "userId": localStorage.getItem('userid')
        })
    }).then(async (response) => {
        if (!response.ok) {
            throw new Error('Failed to update Dislike status');
        }else{
            var data = await response.json();
            console.log(data)
            const spinnerEl = document.querySelector('.spinnerborderindex');
            spinnerEl.style.display = 'none';
            if(data.length==0){
                document.getElementById('NotweetText').style.display = 'block'
            }else{
                const reverseddata =data.reverse();
                reverseddata.forEach(async notification => {
                    await renderUserNotificatio(notification)
                })
            }
            await UpdateNotifications()
        //   alert('Notification gathered successfully')
        }
    }).catch(error => {
        console.error(error);
    });

    async function UpdateNotifications() {
        await fetch('https://localhost:7186/api/User/UpdateUserNotifications', {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer '+localStorage.getItem('token'),
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(localStorage.getItem('userid'))
        }).then(async (response) => {
            if (!response.ok) {
                throw new Error('Failed to update Dislike status');
            }else{
            }
        }).catch(error => {
            console.error(error);
        });
    }
}

function renderUserNotificatio(notification) {

    const head = document.getElementById('header-notifi')

    const header = document.createElement('div');
    header.classList = 'header-post '
    head.style.cursor = "pointer"

    
    header.addEventListener('click', async function() {
        if(notification.tweetType === 'Tweet'){
            localStorage.setItem('TweetType-Tweetdetails','Tweet')
            localStorage.setItem('TweetId-Tweetdetails',notification.tweetId)
            window.location.href = "TweetDetails.html"
        }else if(notification.tweetType === 'Retweet'){
            localStorage.setItem('TweetType-Tweetdetails','Retweet')
            localStorage.setItem('TweetId-Tweetdetails',notification.tweetId)
            window.location.href = "TweetDetails.html"
        }else if(notification.tweetType === 'Follow'){
            localStorage.setItem('profilepagedisplayeruserid',notification.tweetId)
            window.location.href = "TwitterProfile.html"
        }
    })

    const imgdiv = document.createElement('div');
    imgdiv.classList = 'header-img-wrapper'

    const img = document.createElement('img')
    img.src = notification.notificationPost

    imgdiv.appendChild(img)

    const Content = document.createElement('div');
    Content.classList = 'input'
    if(notification.isUserSeen != "Yes"){
        Content.style.backgroundColor = 'rgba(39, 183, 245, 0.32)'
    }
    Content.innerHTML = `${notification.notificatioContent}`

    const Time = document.createElement('div');
    Time.classList = "notifi-time"
    Time.innerHTML = `<span style="font-size:10px;">${timeAgo(notification.contentDateTime)}</span>`
 
    Content.appendChild(Time)

    header.appendChild(imgdiv)

    header.appendChild(Content)


    head.appendChild(header)

}

function timeAgo(date) {
    const now = new Date();
    const tweetDate = new Date(date);
    const diff = now - tweetDate;
  
    const minutes = Math.floor(diff / 1000 / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);
    const months = Math.floor(days / 30);
  
    if (minutes < 1) return 'just now';
    if (minutes < 60) return `${minutes}mins`;
    if (hours < 24) return `${hours}hrs `;
    if (days < 30) return `${days}days`;
    return `${months}mon`;
  }
