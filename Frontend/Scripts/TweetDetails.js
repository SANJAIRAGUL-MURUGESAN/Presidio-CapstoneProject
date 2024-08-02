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


document.getElementById('usernamenav').innerHTML = localStorage.getItem('username')
document.getElementById('userprofileimgnav').src = localStorage.getItem('userprofileimglink')
// document.getElementById('userprofileimgnav2').src = localStorage.getItem('userprofileimglink')
document.getElementById('userimagemodal2').src = localStorage.getItem('userprofileimglink')


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

  document.addEventListener('DOMContentLoaded', function () {
    const inputField = document.getElementById('tweetcontentinput');
    const hashtagSuggestions = document.getElementById('emojiContainer');
  
    inputField.addEventListener('input', function () {
  
        const text = inputField.value;
        const hashIndex = text.lastIndexOf('#');
        const hashIndex1 = text.lastIndexOf('@');
  
        console.log(hashIndex)
  
        if (hashIndex !== -1) {
            const hashtag = text.substring(hashIndex + 1);
            console.log(hashtag)
            if (hashtag.length > 0) {
                // Use dummy data for hashtags
                const dummyHashtags = ['coding', 'javascript', 'webdev', 'frontend', 'backend', 'html', 'css', 'react', 'nodejs'];
                const filteredHashtags = dummyHashtags.filter(tag => tag.startsWith(hashtag));
                console.log(filteredHashtags.length)
                if(filteredHashtags.length>0){
                  showHashtags(filteredHashtags);
                }else if(filteredHashtags.length ==0){
                  hashtagSuggestions.style.display = 'none';
                }
            } else{
              hashtagSuggestions.style.display = 'none'; 
            }
        } else {
          hashtagSuggestions.style.display = 'none';
        }
    });
  
    function showHashtags(hashtags) {
      document.getElementById('emojiContainer').style.display = "flex"
        hashtagSuggestions.innerHTML = '';
  
        hashtags.forEach(tag => {
            const div = document.createElement('div');
            div.textContent = `#${tag}`;
            div.addEventListener('click', function () {
                const text = inputField.value;
                const hashIndex = text.lastIndexOf('#');
                inputField.value = text.substring(0, hashIndex) + '#' + tag + ' ';
                hashtagSuggestions.style.display = 'none';
            });
            hashtagSuggestions.appendChild(div);
        });
    }
  
    // Hide the hashtag suggestions if clicked outside
    document.addEventListener('click', function (event) {
        if (!hashtagSuggestions.contains(event.target) && event.target !== inputField) {
            hashtagSuggestions.style.display = 'none';
        }
    });
  
  });

  // Function to get the mentions starts

document.addEventListener('DOMContentLoaded', function () {
    const inputField = document.getElementById('tweetcontentinput');
    const hashtagSuggestions = document.getElementById('emojiContainer1');
  
    inputField.addEventListener('input', function () {
  
        const text = inputField.value;
        const hashIndex = text.lastIndexOf('@');
  
        console.log(hashIndex)
  
        if (hashIndex !== -1) {
            const hashtag = text.substring(hashIndex + 1);
            console.log(hashtag)
            if (hashtag.length > 0) {
                // Use dummy data for hashtags
                const dummyHashtags = ['sanjai','ragul','gayathri','ritika','presidio','genspark'];
                const filteredHashtags = dummyHashtags.filter(tag => tag.startsWith(hashtag));
                console.log(filteredHashtags.length)
                if(filteredHashtags.length>0){
                  showHashtags(filteredHashtags);
                }else if(filteredHashtags.length ==0){
                  hashtagSuggestions.style.display = 'none';
                }
            } else{
              hashtagSuggestions.style.display = 'none'; 
            }
        } else {
          hashtagSuggestions.style.display = 'none';
        }
    });
  
    function showHashtags(hashtags) {
      document.getElementById('emojiContainer1').style.display = "flex"
        hashtagSuggestions.innerHTML = '';
  
        hashtags.forEach(tag => {
            const div = document.createElement('div');
            div.textContent = `#${tag}`;
            div.addEventListener('click', function () {
                const text = inputField.value;
                const hashIndex = text.lastIndexOf('@');
                inputField.value = text.substring(0, hashIndex) + '@' + tag + ' ';
                hashtagSuggestions.style.display = 'none';
            });
            hashtagSuggestions.appendChild(div);
        });
    }
  
    // Hide the hashtag suggestions if clicked outside
    document.addEventListener('click', function (event) {
        if (!hashtagSuggestions.contains(event.target) && event.target !== inputField) {
            hashtagSuggestions.style.display = 'none';
        }
    });
  
  });

  document.querySelector('.modal-header button').addEventListener('click', function () {
    const postContent = document.getElementById('tweetcontentinput').value;
    console.log(postContent)
    // Regular expressions to match mentions (@username) and comments (#comment)
    let mentionRegex = /@(\w+)/g;
    let commentRegex = /#(\w+)/g;

    // Arrays to store mentions and comments
    let mentions = [];
    let comments = [];

    // Find all mentions in the post content
    let mentionMatches = postContent.match(mentionRegex);
    if (mentionMatches) {
      // Extract usernames and add to mentions array
      mentions = mentionMatches.map(match => match.substring(1)); // Remove "@" from the match
    }

    // Find all comments in the post content
    let commentMatches = postContent.match(commentRegex);
    if (commentMatches) {
      // Extract comments and add to comments array
      comments = commentMatches.map(match => match.substring(1)); // Remove "#" from the match
    }
    console.log(mentions)
    console.log(comments)

    async function addTweetComment(){

        await fetch('https://localhost:7186/api/Comments/AddTweetComment', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             },
            body: JSON.stringify({
                "commentContent": postContent,
                "userId": localStorage.getItem('userid'),
                "tweetId": localStorage.getItem('TweetId-Tweetdetails')
            })
        })
        .then(async res => {
            if (!res.ok) {
                console.log(data.errorCode)
            }else{
                console.log(res)
                Toastify({
                    text: "Hey User, Your Comment Added Successfully!",
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                   }).showToast();
                // alert('Hey User, Your Comment Added Successfully!');
            }
        })
        .catch(error => {
            alert(error);
        });
    }

    async function addReplyReply(){

        await fetch('https://localhost:7186/api/Comments/AddReplytoReply', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             },
            body: JSON.stringify({
                "replyContent": postContent,
                "userId": localStorage.getItem('userid'),
                "replyId": localStorage.getItem('ReplyId'),
                "commentId": localStorage.getItem('CommentId')
            })
        })
        .then(async res => {
            if (!res.ok) {
                console.log(res)
            }else{
                console.log(res)
                localStorage.removeItem('BackendTo')
                Toastify({
                    text: "Hey User, Your Reply Added Successfully!",
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                   }).showToast();
                // alert('Hey User, Your Reply Added Successfully!');
            }
        })
        .catch(error => {
            alert(error);
        });
    }

    async function addCommentReply(){

        await fetch('https://localhost:7186/api/Comments/AddCommentReply', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             },
            body: JSON.stringify({
                "replyType": "comment",
                "replyContent": postContent,
                "userId": localStorage.getItem('userid'),
                "comment_ReplyId": localStorage.getItem('CommentId')
            })
        })
        .then(async res => {
            if (!res.ok) {
                console.log(res.errorCode)
            }else{
                localStorage.removeItem('BackendTo')
                console.log(res)
                Toastify({
                    text: "Hey User, Your Reply Comment Added Successfully!",
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                   }).showToast();
                // alert('Hey User, Your Reply Comment Added Successfully!');
            }
        })
        .catch(error => {
            alert(error);
        });
    }

    if(localStorage.getItem('TweetType-Tweetdetails') === "Tweet"){
        if(localStorage.getItem('BackendTo')=='ReplyComment'){
            addCommentReply()
        }else if(localStorage.getItem('BackendTo')=='ReplyReply'){
            addReplyReply()
        }
        else{
            addTweetComment()
        }
    }else if(localStorage.getItem('TweetType-Tweetdetails') === "Retweet"){
        if(localStorage.getItem('BackendTo')=='ReplyComment'){
            addRetweetCommentReply()
        }else if(localStorage.getItem('BackendTo')=='ReplyReply'){
            RetweetaddReplyReply()
        }
        else{
            addRetweetComment()
        }
    }

    async function addRetweetComment(){

        await fetch('https://localhost:7186/api/Comments/AddRetweetComment', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             },
            body: JSON.stringify({
                "commentContent": postContent,
                "userId": localStorage.getItem('userid'),
                "retweetId": localStorage.getItem('TweetId-Tweetdetails')
            })
        })
        .then(async res => {
            if (!res.ok) {
                console.log(res)
            }else{
                console.log(res)
                Toastify({
                    text: "Hey User, Your Comment Added Successfully!",
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                   }).showToast();
                // alert('Hey User, Your Comment Added Successfully!');
            }
        })
        .catch(error => {
            alert(error);
        });
    }

    async function addRetweetCommentReply(){

        await fetch('https://localhost:7186/api/Comments/AddRetweetCommentReply', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             },
            body: JSON.stringify({
                "replyType": "comment",
                "replyContent": postContent,
                "userId": localStorage.getItem('userid'),
                "comment_ReplyId": localStorage.getItem('CommentId')
            })
        })
        .then(async res => {
            if (!res.ok) {
                console.log(res.errorCode)
            }else{
                localStorage.removeItem('BackendTo')
                console.log(res)
                Toastify({
                    text: "Hey User, Your Reply Comment Added Successfully!",
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                   }).showToast();
                // alert('Hey User, Your Reply Comment Added Successfully!');
            }
        })
        .catch(error => {
            alert(error);
        });
    }

    async function RetweetaddReplyReply(){

        await fetch('https://localhost:7186/api/Comments/AddRetweetCommentReplytoReply', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             },
            body: JSON.stringify({
                "replyContent": postContent,
                "userId": localStorage.getItem('userid'),
                "reweetCommentReplyId": localStorage.getItem('ReplyId'),
                "retweetCommentId": localStorage.getItem('CommentId')
            })
        })
        .then(async res => {
            if (!res.ok) {
                console.log(res)
            }else{
                console.log(res)
                localStorage.removeItem('BackendTo')
                Toastify({
                    text: "Hey User, Your Reply Added Successfully!",
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                   }).showToast();
                // alert('Hey User, Your Reply Added Successfully!');
            }
        })
        .catch(error => {
            alert(error);
        });
    }


  });

  


document.addEventListener('DOMContentLoaded', async function() {

    async function fetchTweets() {

      await fetch('https://localhost:7186/api/Tweet/TweetDetails', {
          method: 'POST',
          headers: {
              'Authorization': 'Bearer '+localStorage.getItem('token'),
              'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            "tweetId": localStorage.getItem('TweetId-Tweetdetails'),
            "tweetType": "Tweet",
            "userId": localStorage.getItem('userid')
        })
      }).then(async (response) => {
          var data = await response.json();
          console.log(data)
          await fetchComments(data)
        //   rendertweet(data)
      }).catch(error => {
          console.error(error);
      });
    }

    async function fetchComments(tweet) {
        // console.log('ghj')
        await fetch('https://localhost:7186/api/Comments/CommentDetails', {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer '+localStorage.getItem('token'),
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
              "tweetId": localStorage.getItem('TweetId-Tweetdetails'),
              "userId": localStorage.getItem('userid'),
          })
        }).then(async (response) => {
            var data = await response.json();
            console.log(data)
            await rendertweet(tweet,data)
            // rendertweet(data)
        }).catch(error => {
            console.error(error);
        });
      }
  

    async function fetchRetweets() {

        await fetch('https://localhost:7186/api/Tweet/RetweetDetails', {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer '+localStorage.getItem('token'),
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "retweetId": localStorage.getItem('TweetId-Tweetdetails'),
                "tweetType": "Retweet",
                "userId": localStorage.getItem('userid')
            })
        }).then(async (response) => {
            var data = await response.json();
            console.log(data);
            // renderRetweet(data)
            fetchRetweetComments(data)
        }).catch(error => {
            console.error(error);
        });
    }

    async function fetchRetweetComments(tweet) {
        await fetch('https://localhost:7186/api/Comments/RetweetCommentDetails', {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer '+localStorage.getItem('token'),
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
              "retweetId": localStorage.getItem('TweetId-Tweetdetails'),
              "userId": localStorage.getItem('userid'),
          })
        }).then(async (response) => {
            var data = await response.json();
            console.log(data)
            await renderRetweet(tweet,data)
            // rendertweet(data)
        }).catch(error => {
            console.error(error);
        });
      }
  

    if(localStorage.getItem('TweetType-Tweetdetails') === "Tweet"){
        await fetchTweets()
    }else if(localStorage.getItem('TweetType-Tweetdetails') === "Retweet"){
        await fetchRetweets()
    }

})

function rendertweet(tweet,comments){
    const postContainer = document.getElementById('post-container');
    // postContainer.innerHTML = ''; 

    const postDiv = document.createElement('div');
    postDiv.className = 'post';

    const userAvatarDiv = document.createElement('div');
    userAvatarDiv.className = 'user-avatar';
    const userAvatarImg = document.createElement('img');
    userAvatarImg.src = tweet.tweetOwnerProfileImgLink;
    userAvatarDiv.appendChild(userAvatarImg);

    const postContentDiv = document.createElement('div');
    postContentDiv.className = 'post-content';

    const postUserInfoDiv = document.createElement('div');
    postUserInfoDiv.className = 'post-user-info';

    const userNameH4 = document.createElement('h4');
    userNameH4.textContent = tweet.tweetOwnerUserName;
    const checkIcon = document.createElement('i');
    checkIcon.className = 'fas fa-check-circle';
    const userHandleSpan = document.createElement('span');
    userHandleSpan.textContent = `@${tweet.tweetOwnerUserId} . ${timeAgo(tweet.tweetDateTime)}`;
    userHandleSpan.style.fontSize = '12px'

    postUserInfoDiv.appendChild(userNameH4);
    postUserInfoDiv.appendChild(checkIcon);
    postUserInfoDiv.appendChild(userHandleSpan);

    const postTextP = document.createElement('p');
    postTextP.className = 'post-text';
    postTextP.innerHTML = tweet.tweetContent.replace(/(@\w+|#\w+)/g, match => {
        return `<span class="highlight">${match}</span>`;
    });

    const postImgDiv = document.createElement('div');
    postImgDiv.className = 'post-img';
    if (tweet.tweetFile1 && tweet.tweetFile1 !== "null") {
        const postImg = document.createElement('img');
        postImg.src = tweet.tweetFile1;
        postImg.alt = 'post';
        postImgDiv.appendChild(postImg);
    }

    const postIconsDiv = document.createElement('div');
    postIconsDiv.className = 'post-icons';

    const commentIcon = document.createElement('i');
    commentIcon.className = 'far fa-comment';
    commentIcon.style.cursor = 'pointer'
    const commentText = document.createElement('h6');
    commentText.style.fontSize = '8px';
    // commentText.textContent = '12 Comments';
    if(tweet.commentsCount==0 || tweet.commentsCount==1){
        commentText.textContent =`${tweet.commentsCount} Comment`;
      }else{
        commentText.textContent =`${tweet.commentsCount} Comments`
    }
    commentIcon.appendChild(commentText);

    commentIcon.addEventListener('click', async function(){
        modal.style.display = 'block'
        modalWrapper.classList.add('modal-wrapper-display')
    })

    const retweetIcon = document.createElement('i');
    retweetIcon.id = 'retweet-icon'; 
    retweetIcon.className = 'fas fa-retweet';
    retweetIcon.style.cursor = 'pointer'
    const retweetText = document.createElement('h6');
    retweetText.style.fontSize = '8px';
    // retweetText.textContent = '3 Retweets';
    if(tweet.retweetsCount == 0 || tweet.retweetsCount == 1){
        retweetText.textContent = `${tweet.retweetsCount} Retweet`;
      }else{
        retweetText.textContent = `${tweet.retweetsCount} Retweets`;
      }
    retweetIcon.appendChild(retweetText);

    retweetIcon.addEventListener('click', async function() {
      modal.style.display = 'block'
      modalWrapper.classList.add('modal-wrapper-display')
  
      if(modalInput.value !== ''){
          modalInput.value = '';
          changeOpacity(0.5)
      }

      localStorage.setItem("actualtweetid",tweet.tweetId);
  });

    const likeIcon = document.createElement('i');
    likeIcon.className = 'fa-regular fa-heart';
    likeIcon.style.color = '#3e6f6f'
    likeIcon.style.cursor = 'pointer'
    const likeText = document.createElement('h6');
    likeText.style.fontSize = '8px';
    if(tweet.tweetLikesCount==0 || tweet.tweetLikesCount==1){
        likeText.textContent = `${tweet.tweetLikesCount} Like`;
    }else{
        likeText.textContent = `${tweet.tweetLikesCount} Likes`;
    }
    // likeText.textContent = `${tweet.tweetLikesCount} Likes`;
    likeText.style.color  = 'grey'
    likeIcon.appendChild(likeText);

    if(tweet.isTweetLikedByUser == 'Yes'){
      likeIcon.className = 'fa-solid fa-heart';
      likeIcon.style.color = '#ed0c0c'
    }

    likeIcon.addEventListener('click', async function() {
      if (likeIcon.classList.contains('fa-regular')) {
        likeIcon.className = 'fa-solid fa-heart';
        likeIcon.style.color = '#ed0c0c'
          await updateLikeStatus(tweet.tweetId);
      }else if(likeIcon.classList.contains('fa-heart')){
        likeIcon.className = 'fa-regular fa-heart';
        likeIcon.style.color = '#3e6f6f'
        updateTweetDisLikeStatus(tweet.tweetId)
      }
  });

    const shareIcon = document.createElement('i');
    shareIcon.className = 'fas fa-share-alt';
    const shareText = document.createElement('h6');
    shareText.style.fontSize = '8px';
    shareText.textContent = 'share'
    shareIcon.appendChild(shareText)

    postIconsDiv.appendChild(commentIcon);
    postIconsDiv.appendChild(retweetIcon);
    postIconsDiv.appendChild(likeIcon);
    postIconsDiv.appendChild(shareIcon);

    postContentDiv.appendChild(postUserInfoDiv);
    postContentDiv.appendChild(postTextP);
    postContentDiv.appendChild(postImgDiv);
    postContentDiv.appendChild(postIconsDiv);

    postDiv.appendChild(userAvatarDiv);
    postDiv.appendChild(postContentDiv);

    postContainer.appendChild(postDiv);

    //Comments

    const header = document.createElement('div');
    header.className = 'header-post'

    const inputContainer = document.getElementById('input-container');
    inputContainer.innerHTML = ''; // Clear existing posts

    comments.forEach(post => {
        // Create postc div
        const postcDiv = document.createElement('div');
        postcDiv.className = 'postc';

        const userAvatarDiv = document.createElement('div');
        userAvatarDiv.className = 'user-avatar';
        const userAvatarImg = document.createElement('img');
        userAvatarImg.src = post.userProfileLink;
        userAvatarDiv.appendChild(userAvatarImg);

        const postContentDiv = document.createElement('div');
        postContentDiv.className = 'post-content';

        const postUserInfoDiv = document.createElement('div');
        postUserInfoDiv.className = 'post-user-info';
        const userNameH4 = document.createElement('div');
        userNameH4.innerHTML = `<span style="font-size:14px; font-weight:bold"> ${post.userName} </span>. ${timeAgo(post.commentDateTime)}`;
        postUserInfoDiv.appendChild(userNameH4);

        const commentTextP = document.createElement('p');
        commentTextP.className = 'commenttext';
        commentTextP.style.fontSize = "14px"
        commentTextP.textContent = post.commentContent;

        const postIconsDiv = document.createElement('div');
        postIconsDiv.className = 'post-icons';
        const commentIcon = document.createElement('i');
        commentIcon.className = 'fa fa-reply';
        commentIcon.style.cursor = 'pointer';

        commentIcon.addEventListener('click', async function() {
            localStorage.setItem('BackendTo','ReplyComment')
            localStorage.setItem('CommentId',post.commentId)
            const reply = document.getElementById('replyshowmodal')
            reply.style.display = "flex"
            const replycontent = document.getElementById('tweetcontentinput2')
            replycontent.style.fontSize = "15px"
            document.getElementById('userimagemodal').src = post.userProfileLink
            replycontent.innerHTML = post.commentContent
            modal.style.display = 'block'
            modalWrapper.classList.add('modal-wrapper-display')

        })


        const likeIcon = document.createElement('i');
        likeIcon.className = 'fa-regular fa-heart';
        likeIcon.style.color = '#3e6f6f'
        likeIcon.style.cursor = 'pointer'
        const likeText = document.createElement('h6');
        likeText.style.fontSize = '8px';
        if(post.likesCount ==0 || post.likesCount==1){
            likeText.textContent = `${post.likesCount} Like`;
        }else{
            likeText.textContent = `${post.likesCount} Likes`;
        }
        // likeText.textContent = `${post.likesCount} Likes`;
        likeText.style.color  = 'grey'
        likeIcon.appendChild(likeText);

        if(post.isLikedByUser == 'Yes'){
            likeIcon.className = 'fa-solid fa-heart';
            likeIcon.style.color = '#ed0c0c'
        }


        likeIcon.addEventListener('click', async function() {
            if (likeIcon.classList.contains('fa-regular')) {
              likeIcon.className = 'fa-solid fa-heart';
              likeIcon.style.color = '#ed0c0c'
                await updateTweetCommentLikeStatus(post.commentId);
            }else if(likeIcon.classList.contains('fa-heart')){
              likeIcon.className = 'fa-regular fa-heart';
              likeIcon.style.color = '#3e6f6f'
              updateTweetCommentDisLikeStatus(post.commentId)
            }
        });


        postIconsDiv.appendChild(commentIcon);
        postIconsDiv.appendChild(likeIcon);

        postContentDiv.appendChild(postUserInfoDiv);
        postContentDiv.appendChild(commentTextP);
        postContentDiv.appendChild(postIconsDiv);

        postcDiv.appendChild(userAvatarDiv);
        postcDiv.appendChild(postContentDiv);

        inputContainer.appendChild(postcDiv);

        var commentID = post.commentId;
        console.log('commentid',commentID)

        if(post.replies.length > 0){

        post.replies.forEach(post => {

        // Create postr div
        const postrDiv = document.createElement('div');
        postrDiv.className = 'postr';

        const userAvatarDiv2 = document.createElement('div');
        userAvatarDiv2.className = 'user-avatar';
        const userAvatarImg2 = document.createElement('img');
        userAvatarImg2.src = post.userProfileImageLink;
        userAvatarDiv2.appendChild(userAvatarImg2);

        const postContentDiv2 = document.createElement('div');
        postContentDiv2.className = 'post-content';

        const postUserInfoDiv2 = document.createElement('div');
        postUserInfoDiv2.className = 'post-user-info';
        const userNameH42 = document.createElement('div');
        userNameH42.innerHTML = `<span style="font-size:14px; font-weight:bold"> ${post.userName} </span>. ${timeAgo(post.replyDateTime)}`;
        // userNameH42.textContent = `${post.userName} . ${timeAgo(post.replyDateTime)}`;
        postUserInfoDiv2.appendChild(userNameH42);

        const commentTextP2 = document.createElement('p');
        commentTextP2.className = 'commenttext';
        commentTextP2.style.fontSize = "14px"
        commentTextP2.textContent = post.replyContent;


        const postIconsDivr = document.createElement('div');
        postIconsDivr.className = 'post-icons';
        const commentIconr = document.createElement('i');
        commentIconr.className = 'fa fa-reply';
        commentIconr.style.cursor = 'pointer';
        
        const likeIconr = document.createElement('i');
        likeIconr.className = 'fa-regular fa-heart';
        likeIconr.style.color = '#3e6f6f'
        likeIconr.style.cursor = 'pointer'
        const likeTextr = document.createElement('h6');
        likeTextr.style.fontSize = '8px';
        if(post.likedCount ==0 || post.likedCount==1){
            likeTextr.textContent = `${post.likedCount} Like`;
        }else{
            likeTextr.textContent = `${post.likedCount} Likes`;
        }
        // likeTextr.textContent = `${post.likedCount} Likes`;
        likeTextr.style.color  = 'grey'
        likeIconr.appendChild(likeTextr);

        console.log(post.isLikedByUser)
        if(post.isLikedByUser == 'Yes'){
            likeIconr.className = 'fa-solid fa-heart';
            likeIconr.style.color = '#ed0c0c'
        }

        likeIconr.addEventListener('click', async function() {
            if (likeIconr.classList.contains('fa-regular')) {
              likeIconr.className = 'fa-solid fa-heart';
              likeIconr.style.color = '#ed0c0c'
                await updateTweetCommentReplyLikeStatus(post.id);
            }else if(likeIconr.classList.contains('fa-heart')){
              likeIconr.className = 'fa-regular fa-heart';
              likeIconr.style.color = '#3e6f6f'
              updateTweetCommentReplyDisLikeStatus(post.id)
            //   updateTweetCommentDisLikeStatus(post.commentId)
            }
        });



        postIconsDivr.appendChild(commentIconr);
        postIconsDivr.appendChild(likeIconr);

        commentIconr.addEventListener('click', async function() {
            localStorage.setItem('BackendTo','ReplyReply')
            localStorage.setItem('ReplyId',post.id)
            localStorage.setItem('CommentId',commentID);
            const reply = document.getElementById('replyshowmodal')
            reply.style.display = "flex"
            document.getElementById('userimagemodal').src = post.userProfileImageLink
            const replycontent = document.getElementById('tweetcontentinput2')
            replycontent.style.fontSize = "15px"
            replycontent.innerHTML = post.replyContent
            modal.style.display = 'block'
            modalWrapper.classList.add('modal-wrapper-display')
        })

        postContentDiv2.appendChild(postUserInfoDiv2);
        postContentDiv2.appendChild(commentTextP2);
        postContentDiv2.appendChild(postIconsDivr)

        postrDiv.appendChild(userAvatarDiv2);
        postrDiv.appendChild(postContentDiv2);

        inputContainer.appendChild(postrDiv);
        });
        }
    });
    header.appendChild(inputContainer)
    postContainer.appendChild(header);

    document.querySelectorAll('.highlight').forEach(element => {
      element.addEventListener('click', function() {
          handleMentionClick(element.textContent);
      });
  });
}

function renderRetweet(tweet,comments) {
    const postContainer = document.getElementById('post-container');
    // postContainer.innerHTML = ''; // Clear existing posts
  
    // retweets.forEach(tweet => {
      
        const postDiv = document.createElement('div');
        postDiv.className = 'post';
  
        const userAvatarDiv = document.createElement('div');
        userAvatarDiv.className = 'user-avatar';
        const userAvatarImg = document.createElement('img');
        userAvatarImg.src = tweet.retweetUserProfileImgLink;
        userAvatarDiv.appendChild(userAvatarImg);
  
        const postContentDiv = document.createElement('div');
        postContentDiv.className = 'post-content';
  
        const postUserInfoDiv = document.createElement('div');
        postUserInfoDiv.className = 'post-user-info';
  
        const userNameH4 = document.createElement('h4');
        userNameH4.textContent = tweet.retweetUserName;
        const checkIcon = document.createElement('i');
        checkIcon.className = 'fas fa-check-circle';
        const userHandleSpan = document.createElement('span');
        userHandleSpan.textContent = `@${tweet.retweetUserId} . Reposted ${timeAgo(tweet.retweetDateTime)}`;
        userHandleSpan.style.fontSize = '12px'
  
        postUserInfoDiv.appendChild(userNameH4);
        postUserInfoDiv.appendChild(checkIcon);
        postUserInfoDiv.appendChild(userHandleSpan);
  
        const postTextP = document.createElement('p');
        postTextP.className = 'post-text';
        postTextP.innerHTML = tweet.retweetContent.replace(/(@\w+|#\w+)/g, match => {
            return `<span class="highlight">${match}</span>`;
        });
        const line = document.createElement('div');
        line.style.height= '2px';
        line.style.backgroundColor= '#3498db';
        //
  
        const userAvatarDiv1 = document.createElement('div');
        userAvatarDiv1.className = 'user-avatar';
        const userAvatarImg1 = document.createElement('img');
        userAvatarImg1.src = tweet.tweetOwnerProfileImgLink;
        userAvatarDiv1.appendChild(userAvatarImg1);
  
        const postContentDiv1 = document.createElement('div');
        postContentDiv1.className = 'post-content';
  
        const postUserInfoDiv1 = document.createElement('div');
        postUserInfoDiv1.className = 'post-user-info';
  
        const userNameH41 = document.createElement('h4');
        const imageElement = document.createElement('img');
        userNameH41.textContent = ` ${tweet.tweetOwnerUserName}`;
        userNameH41.style.marginLeft = '6px';
        const checkIcon1 = document.createElement('i');
        checkIcon1.className = 'fas fa-check-circle';
        const userHandleSpan1 = document.createElement('span');
        userHandleSpan1.appendChild(imageElement);
        userHandleSpan1.textContent = `@${tweet.tweetOwnerUserId} . ${timeAgo(tweet.tweetDateTime)}`;
        userHandleSpan1.style.fontSize = '12px'
  
        const smallImage = document.createElement('img');
        smallImage.src = tweet.tweetOwnerProfileImgLink; // Replace with the actual path to the small image
        smallImage.alt = 'small image';
        smallImage.style.width = '50px'; // Adjust the size as needed
        smallImage.style.height = '50px'; // Adjust the size as needed
        smallImage.style.borderRadius = '50%';
        // Optional: Add some margin for spacing
        postUserInfoDiv1.appendChild(smallImage);
  
        //
  
        postUserInfoDiv1.appendChild(userNameH41);
        postUserInfoDiv1.appendChild(checkIcon1);
        postUserInfoDiv1.appendChild(userHandleSpan1);
  
        const postTextP1 = document.createElement('p');
        postTextP1.className = 'post-text';
        postTextP1.innerHTML = tweet.tweetContent.replace(/(@\w+|#\w+)/g, match => {
            return `<span class="highlight">${match}</span>`;
        });
  
        const postImgDiv = document.createElement('div');
        postImgDiv.className = 'post-img';
        if (tweet.tweetFile1 && tweet.tweetFile1 !== "null") {
            const postImg = document.createElement('img');
            postImg.src = tweet.tweetFile1;
            postImg.alt = 'post';
            postImgDiv.appendChild(postImg);
        }
  
        const postIconsDiv = document.createElement('div');
        postIconsDiv.className = 'post-icons';
  
        const commentIcon = document.createElement('i');
        commentIcon.className = 'far fa-comment';
        commentIcon.style.cursor = 'pointer'
        const commentText = document.createElement('h6');
        commentText.style.fontSize = '8px';
        // commentText.textContent = '12 Comments';
        if(tweet.commentsCount==0 || tweet.commentsCount==1){
            commentText.textContent =`${tweet.commentsCount} Comment`;
          }else{
            commentText.textContent =`${tweet.commentsCount} Comments`
        }
        commentIcon.appendChild(commentText);

        commentIcon.addEventListener('click', async function(){
            modal.style.display = 'block'
            modalWrapper.classList.add('modal-wrapper-display')
        })
  
        const retweetIcon = document.createElement('i');
        retweetIcon.id = 'retweet-icon'; 
        retweetIcon.className = 'fas fa-retweet';
        retweetIcon.style.cursor = 'pointer'
        const retweetText = document.createElement('h6');
        retweetText.style.fontSize = '8px';
        retweetText.textContent = '3 Retweets';
        retweetIcon.appendChild(retweetText);
  
        retweetIcon.addEventListener('click', async function() {
          modal.style.display = 'block'
          modalWrapper.classList.add('modal-wrapper-display')
      
          if(modalInput.value !== ''){
              modalInput.value = '';
              changeOpacity(0.5)
          }
  
          localStorage.setItem("actualtweetid",tweet.tweetId);
      });
  
        const likeIcon = document.createElement('i');
        likeIcon.className = 'fa-regular fa-heart';
        likeIcon.style.color = '#3e6f6f'
        likeIcon.style.cursor = 'pointer'
        const likeText = document.createElement('h6');
        likeText.style.fontSize = '8px';
        if(tweet.retweetLikesCount==1 || tweet.retweetLikesCount==0){
            likeText.textContent = `${tweet.retweetLikesCount} Like`;
        }else{
            likeText.textContent = `${tweet.retweetLikesCount} Likes`;
        }
        // likeText.textContent = `${tweet.retweetLikesCount} Likes`;
        likeText.style.color  = 'grey'
        likeIcon.appendChild(likeText);
  
        if(tweet.isRetweetLikedByUser == 'Yes'){
          likeIcon.className = 'fa-solid fa-heart';
          likeIcon.style.color = '#ed0c0c'
        }
    
  
        likeIcon.addEventListener('click', async function() {
          if (likeIcon.classList.contains('fa-regular')) {
            likeIcon.className = 'fa-solid fa-heart';
            likeIcon.style.color = '#ed0c0c'
              await updateRetweetLikeStatus(tweet.retweetId);
          }else if(likeIcon.classList.contains('fa-heart')){
            likeIcon.className = 'fa-regular fa-heart';
            likeIcon.style.color = '#3e6f6f'
            updateRetweetDisLikeStatus(tweet.retweetId)
          }
      });
  
        const shareIcon = document.createElement('i');
        shareIcon.className = 'fas fa-share-alt';
  
        postIconsDiv.appendChild(commentIcon);
        postIconsDiv.appendChild(retweetIcon);
        postIconsDiv.appendChild(likeIcon);
        postIconsDiv.appendChild(shareIcon);
  
        postContentDiv.appendChild(postUserInfoDiv);
        postContentDiv.appendChild(postTextP);
        postContentDiv.appendChild(line);
  
  
        postContentDiv.appendChild(postUserInfoDiv1);
        postContentDiv.appendChild(postTextP1);
  
        postContentDiv.appendChild(postImgDiv);
        postContentDiv.appendChild(postIconsDiv);
  
        postDiv.appendChild(userAvatarDiv);
  
  
        postDiv.appendChild(postContentDiv);
  
        postContainer.appendChild(postDiv);

         //Comments

    const header = document.createElement('div');
    header.className = 'header-post'

    const inputContainer = document.getElementById('input-container');
    inputContainer.innerHTML = ''; // Clear existing posts

    comments.forEach(post => {
        console.log('data',post.userName)
        // Create postc div
        const postcDiv = document.createElement('div');
        postcDiv.className = 'postc';

        const userAvatarDiv = document.createElement('div');
        userAvatarDiv.className = 'user-avatar';
        const userAvatarImg = document.createElement('img');
        userAvatarImg.src = post.userProfileLink;
        userAvatarDiv.appendChild(userAvatarImg);

        const postContentDiv = document.createElement('div');
        postContentDiv.className = 'post-content';

        const postUserInfoDiv = document.createElement('div');
        postUserInfoDiv.className = 'post-user-info';
        const userNameH4 = document.createElement('div');
        userNameH4.innerHTML = `<span style="font-size:14px; font-weight:bold"> ${post.userName} </span>. ${timeAgo(post.commentDateTime)}`;
        // userNameH4.textContent = post.userName;
        postUserInfoDiv.appendChild(userNameH4);

        const commentTextP = document.createElement('p');
        commentTextP.className = 'commenttext';
        commentTextP.style.fontSize = "14px"
        commentTextP.textContent = post.commentContent;

        const postIconsDiv = document.createElement('div');
        postIconsDiv.className = 'post-icons';
        const commentIcon = document.createElement('i');
        commentIcon.className = 'fa fa-reply';
        commentIcon.style.cursor = 'pointer';

        commentIcon.addEventListener('click', async function() {
            localStorage.setItem('BackendTo','ReplyComment')
            localStorage.setItem('CommentId',post.commentId)
            const reply = document.getElementById('replyshowmodal')
            reply.style.display = "flex"
            const replycontent = document.getElementById('tweetcontentinput2')
            document.getElementById('userimagemodal').src = post.userProfileLink
            replycontent.style.fontSize = "15px"
            replycontent.innerHTML = post.commentContent
            modal.style.display = 'block'
            modalWrapper.classList.add('modal-wrapper-display')
        })


        const likeIcon = document.createElement('i');
        likeIcon.className = 'fa-regular fa-heart';
        likeIcon.style.color = '#3e6f6f'
        likeIcon.style.cursor = 'pointer'
        const likeText = document.createElement('h6');
        likeText.style.fontSize = '8px';
        if(post.likesCount==1 || post.likesCount==0){
            likeText.textContent = `${post.likesCount} Like`;
        }else{
            likeText.textContent = `${post.likesCount} Likes`;
        }
        likeText.style.color  = 'grey'
        likeIcon.appendChild(likeText);

        if(post.isLikedByUser == 'Yes'){
            likeIcon.className = 'fa-solid fa-heart';
            likeIcon.style.color = '#ed0c0c'
        }

        likeIcon.addEventListener('click', async function() {
            if (likeIcon.classList.contains('fa-regular')) {
              likeIcon.className = 'fa-solid fa-heart';
              likeIcon.style.color = '#ed0c0c'
              await updateRetweetCommentLikeStatus(post.commentId)
                // await updateTweetCommentLikeStatus(post.commentId);
            }else if(likeIcon.classList.contains('fa-heart')){
              likeIcon.className = 'fa-regular fa-heart';
              likeIcon.style.color = '#3e6f6f'
              await updateRetweetCommentDislikeLikeStatus(post.commentId)
            //   updateTweetCommentDisLikeStatus(post.commentId)
            }
        });


        postIconsDiv.appendChild(commentIcon);
        postIconsDiv.appendChild(likeIcon);

        postContentDiv.appendChild(postUserInfoDiv);
        postContentDiv.appendChild(commentTextP);
        postContentDiv.appendChild(postIconsDiv);

        postcDiv.appendChild(userAvatarDiv);
        postcDiv.appendChild(postContentDiv);

        inputContainer.appendChild(postcDiv);

        var commentID = post.commentId;
        console.log('commentid',commentID)

        if(post.replies.length > 0){

        post.replies.forEach(post => {

        // Create postr div
        const postrDiv = document.createElement('div');
        postrDiv.className = 'postr';

        const userAvatarDiv2 = document.createElement('div');
        userAvatarDiv2.className = 'user-avatar';
        const userAvatarImg2 = document.createElement('img');
        userAvatarImg2.src = post.userProfileImageLink;
        userAvatarDiv2.appendChild(userAvatarImg2);

        const postContentDiv2 = document.createElement('div');
        postContentDiv2.className = 'post-content';

        const postUserInfoDiv2 = document.createElement('div');
        postUserInfoDiv2.className = 'post-user-info';
        const userNameH42 = document.createElement('div');
        userNameH42.innerHTML = `<span style="font-size:14px; font-weight:bold"> ${post.userName} </span>. ${timeAgo(post.replyDateTime)}`;
        // userNameH42.textContent = post.userName;
        postUserInfoDiv2.appendChild(userNameH42);

        const commentTextP2 = document.createElement('p');
        commentTextP2.className = 'commenttext';
        commentTextP2.style.fontSize = "14px"
        commentTextP2.textContent = post.replyContent;


        const postIconsDivr = document.createElement('div');
        postIconsDivr.className = 'post-icons';

        const likeIconr = document.createElement('i');
        likeIconr.className = 'fa-regular fa-heart';
        likeIconr.style.color = '#3e6f6f'
        likeIconr.style.cursor = 'pointer'
        const likeTextr = document.createElement('h6');
        likeTextr.style.fontSize = '8px';
        if(post.likedCount ==0 || post.likedCount==1){
            likeTextr.textContent = `${post.likedCount} Like`;
        }else{
            likeTextr.textContent = `${post.likedCount} Likes`;
        }
        likeTextr.style.color  = 'grey'
        likeIconr.appendChild(likeTextr);

        if(post.isLikedByUser == 'Yes'){
            likeIconr.className = 'fa-solid fa-heart';
            likeIconr.style.color = '#ed0c0c'
        }

        likeIconr.addEventListener('click', async function() {
            if (likeIconr.classList.contains('fa-regular')) {
              likeIconr.className = 'fa-solid fa-heart';
              likeIconr.style.color = '#ed0c0c'
              updateRetweetCommentReplyLikeStatus(post.id);
                // await updateTweetCommentReplyLikeStatus(post.id);
            }else if(likeIconr.classList.contains('fa-heart')){
              likeIconr.className = 'fa-regular fa-heart';
              likeIconr.style.color = '#3e6f6f'
              await updateRetweetCommentReplyDisLikeStatus(post.id)
            //   updateTweetCommentReplyDisLikeStatus(post.id)
            }
        });

        const commentIconr = document.createElement('i');
        commentIconr.className = 'fa fa-reply';
        commentIconr.style.cursor = 'pointer';
        postIconsDivr.appendChild(commentIconr);
        postIconsDivr.appendChild(likeIconr);

        commentIconr.addEventListener('click', async function() {
            localStorage.setItem('BackendTo','ReplyReply')
            localStorage.setItem('ReplyId',post.id)
            localStorage.setItem('CommentId',commentID);
            const reply = document.getElementById('replyshowmodal')
            reply.style.display = "flex"
            const replycontent = document.getElementById('tweetcontentinput2')
            document.getElementById('userimagemodal').src = post.userProfileImageLink
             replycontent.style.fontSize = "15px"
            replycontent.innerHTML = post.replyContent
            modal.style.display = 'block'
            modalWrapper.classList.add('modal-wrapper-display')
        })

        postContentDiv2.appendChild(postUserInfoDiv2);
        postContentDiv2.appendChild(commentTextP2);
        postContentDiv2.appendChild(postIconsDivr)

        postrDiv.appendChild(userAvatarDiv2);
        postrDiv.appendChild(postContentDiv2);

        inputContainer.appendChild(postrDiv);
        });
        }
    });
    header.appendChild(inputContainer)
    postContainer.appendChild(header);

  
        document.querySelectorAll('.highlight').forEach(element => {
          element.addEventListener('click', function() {
              handleMentionClick(element.textContent);
          });
      });
    // }); 
  }

  
// likes and dislikes

async function updateLikeStatus(tweetId) {
    await fetch('https://localhost:7186/api/Tweet/AddTweetLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "tweetId": tweetId
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update like status!",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Like Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }

async function updateTweetDisLikeStatus(tweetId) {
    await fetch('https://localhost:7186/api/Tweet/AddTweetDisLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "tweetId": tweetId,
          "likedUserId": localStorage.getItem('userid')
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Dislike status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update Dislike status');
        }else{
        //   alert('Dislike Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }
  
  async function updateRetweetLikeStatus(tweetId) {
    await fetch('https://localhost:7186/api/Tweet/AddReTweetLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "retweetId": tweetId
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update like status!",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Like Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }
  
  async function updateRetweetDisLikeStatus(tweetId) {
    await fetch('https://localhost:7186/api/Tweet/AddReTweetDisLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "retweetId": tweetId,
          "likedUserId": localStorage.getItem('userid')
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Dislike status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update Dislike status');
        }else{
        //   alert('Dislike Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }

//   TweetComment Like starts

async function updateTweetCommentLikeStatus(commentId) {
    await fetch('https://localhost:7186/api/Tweet/AddTweetCommentLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "commentId": commentId
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Like status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Comment - Like Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }

  //   TweetComment Like Ends

  //   TweetComment Dislike starts
  async function updateTweetCommentDisLikeStatus(commentId) {
    await fetch('https://localhost:7186/api/Tweet/AddTweetCommentDislike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "commentId": commentId
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Like status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Comment - Dislike Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }
  //   TweetComment Dislike ends

  async function updateTweetCommentReplyLikeStatus(replyid) {
    await fetch('https://localhost:7186/api/Tweet/AddTweetCommentReplyLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "replyId": replyid
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Like status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Reply - Like Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }

   //   TweetComment Reply Dislike starts
   async function updateTweetCommentReplyDisLikeStatus(replyid) {
    await fetch('https://localhost:7186/api/Tweet/AddTweetCommentReplyDislike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "replyId": replyid
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Dislike status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            throw new Error('Failed to update like status');
        }else{
        //   alert('Reply- Dislike Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }
  //   TweetComment Reply Dislike ends

  // Retweet comments and Reply Likes starts

  // Retweet comments Likes starts

  async function updateRetweetCommentLikeStatus(commentId) {
    await fetch('https://localhost:7186/api/Tweet/AddRetweetCommentLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "retweetCommentId": commentId
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Like status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Comment - Like Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }

  // Retweet comments  Likes Ends

  // Retweet comments DisLikes Starts

  async function updateRetweetCommentDislikeLikeStatus(commentId) {
    await fetch('https://localhost:7186/api/Tweet/AddRetweetCommentDislike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "retweetCommentId": commentId
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Dislike status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Comment - DisLike Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }
  // Retweet comments DisLikes Starts

  // Retweet comments Reply DisLikes Starts

  async function updateRetweetCommentReplyLikeStatus(replyid) {
    await fetch('https://localhost:7186/api/Tweet/AddRetweetCommentReplyLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "replyCommentReplyId": replyid
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Like status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Reply - Like Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }
   // Retweet comments Reply DisLikes Ends

    // Retweet comments Reply DisLikes Starts

  async function updateRetweetCommentReplyDisLikeStatus(replyid) {
    await fetch('https://localhost:7186/api/Tweet/AddRetweetCommentReplyDisLike', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "likedUserId": localStorage.getItem('userid'),
          "replyCommentReplyId": replyid
      })
    }).then(response => {
        if (!response.ok) {
            Toastify({
                text: "Hey User, Failed to update Dislike status",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
            // throw new Error('Failed to update like status');
        }else{
        //   alert('Reply - Dislike Added Successfully')
        }
    }).catch(error => {
        console.error(error);
    });
  }
   // Retweet comments Reply DisLikes Ends