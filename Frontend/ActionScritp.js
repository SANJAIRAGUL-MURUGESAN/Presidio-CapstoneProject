// Function to set the images url - Starts

document.getElementById('userprofileimgnav').src = localStorage.getItem('userprofileimglink')
document.getElementById('userprofileimgnav2').src = localStorage.getItem('userprofileimglink')
document.getElementById('userimagemodal').src = localStorage.getItem('userprofileimglink')
document.getElementById('usernamenav').innerHTML = localStorage.getItem('username')

// Function to set the images url - Ends




// Function to Upload images and content starts
let uploadedFiles = [];

document.getElementById('imageIcon').addEventListener('click', function () {
    document.getElementById('fileInput').click();
});

document.getElementById('fileInput').addEventListener('change', function (event) {
    const files = event.target.files;
    const maxFiles = 4;
    const imagePreviewContainer = document.getElementById('imagePreviewContainer');

    if (uploadedFiles.length + files.length > maxFiles) {
        alert('You can only upload a maximum of 2 images');
        return;
    }

    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        uploadedFiles.push(file);

        const reader = new FileReader();
        reader.onload = function (e) {
            const img = document.createElement('img');
            img.src = e.target.result;
            img.className = 'image-preview';
            imagePreviewContainer.appendChild(img);
        };
        reader.readAsDataURL(file);
    }
});

document.querySelector('.modal-header button').addEventListener('click', function () {
    const formData = new FormData();

    const postContent = document.getElementById('tweetcontentinput').value;

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

    var isCommentEnabled = 1;
    if(document.getElementById('commentenable').style.display=='none'){
      isCommentEnabled = 0
    }
    console.log(isCommentEnabled)

    if (uploadedFiles.length > 2 ) {
        alert('Please upload between 0 and 2 images.');
        return;
    }

    for (let i = 0; i < uploadedFiles.length; i++) {
      console.log(uploadedFiles[i])
        formData.append('Images', uploadedFiles[i]);
    }
    
    async function addFeedContent(){

      await fetch('https://localhost:7186/api/Tweet/AddTweetContent', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json',
           },
          body: JSON.stringify({
              "userId": localStorage.getItem('userid'),
              "tweetContent": postContent,
              "isCommentEnable": "yes",
              "tweetHashtags": comments,
              "tweetMentions": mentions
          })
      })
      .then(async res => {
          const data = await res.json();
          if (!res.ok) {
              console.log(data.errorCode)
          }else{
              console.log(data.tweetId)
              if(uploadedFiles.length > 0){
                formData.append('TweetId', data.tweetId);
                await addFeedImages();
              }else{
                alert('Hey User, Your Feed Added Successfully!');
              }
          }
      })
      .catch(error => {
          alert(error);
      });
  }

  async function addRetweetContent(actualtweetid){

    await fetch('https://localhost:7186/api/Tweet/AddRetweetContent', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
         },
        body: JSON.stringify({
            "retweetContent": postContent,
            "isCommentEnable": "Yes",
            "actualTweetId": actualtweetid,
            "userId": localStorage.getItem('userid')
        })
    })
    .then(async res => {
        console.log(res)
        if (!res.ok) {
            console.log(data.errorCode)
        }else{
          localStorage.removeItem('actualtweetid');
          alert('Hey User, Your Repost Added Successfully!');
        }
    })
    .catch(error => {
        alert(error);
    });
}

  async function addFeedImages(){
        fetch('https://localhost:7186/api/Tweet/AddTweetImage', {
            method: 'POST',
            body: formData
        })
        .then(data => {
            console.log(data);
            alert('Hey User, Your Feed Added Successfully!!');
            // Reset the uploaded files and previews
            uploadedFiles = [];
            document.getElementById('imagePreviewContainer').innerHTML = '';
            document.getElementById('fileInput').value = '';
        })
        .catch(error => {
            console.error(error);
            alert('There was an error uploading the images.');
        });
  }

  const isNormalTweet = localStorage.getItem('actualtweetid')

  if(isNormalTweet == undefined){
    console.log("NormalTweet")
    addFeedContent()
  }else{
    console.log("Retweet",isNormalTweet)
    addRetweetContent(isNormalTweet)
  }



});

// Function to upload images and content ends

// Function to get the comments starts
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

// Function to get the comments ends

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


// Function to get the comments ends

// Function to enable and disable comment starts
document.getElementById('commentenable').addEventListener('click', function () {
  document.getElementById('commentdisable').style.display = "block";
  document.getElementById('commentenable').style.display = "none"
});

document.getElementById('commentdisable').addEventListener('click', function () {
  document.getElementById('commentenable').style.display = "block";
  document.getElementById('commentdisable').style.display = "none"
});


// Function to enable and disable comment ends

// Function to get the Feeds - starts

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
          throw new Error('Failed to update like status');
      }else{
        alert('Like Added Successfully')
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
          throw new Error('Failed to update Dislike status');
      }else{
        alert('Dislike Added Successfully')
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
          throw new Error('Failed to update like status');
      }else{
        alert('Like Added Successfully')
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
          throw new Error('Failed to update Dislike status');
      }else{
        alert('Dislike Added Successfully')
      }
  }).catch(error => {
      console.error(error);
  });
}

function handleMentionClick(mention) {
  console.log(`Mention clicked: ${mention}`);
  // Make a backend route call for mention click
  // Example:
  // await fetch(`https://localhost:7186/api/Tweet/Mention/${mention}`, {
  //     method: 'GET',
  //     headers: {
  //         'Authorization': 'Bearer ' + localStorage.getItem('token')
  //     }
  // }).then(response => {
  //     if (!response.ok) {
  //         throw new Error('Failed to handle mention click');
  //     }
  // }).catch(error => {
  //     console.error(error);
  // });
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



document.addEventListener('DOMContentLoaded', async function() {

    async function fetchTweets() {

      await fetch('https://localhost:7186/api/Tweet/Feeds', {
          method: 'POST',
          headers: {
              'Authorization': 'Bearer '+localStorage.getItem('token'),
              'Content-Type': 'application/json',
          },
          body: JSON.stringify(localStorage.getItem('userid'))
      }).then(async (response) => {
          var data = await response.json();
          console.log(data.retweets);
          console.log(data.tweets);
          renderRetweets(data.retweets,data.tweets)
          // renderTweets(data.tweets) 
          // data.forEach(element => {
          //     console.log(element)
          //     renderProducts(element);
          // });
      }).catch(error => {
          console.error(error);
      });
    }
    await fetchTweets()
})

// Function to get the Feeds - ends

function renderRetweets(retweets,tweets) {
  const postContainer = document.getElementById('post-container');
  postContainer.innerHTML = ''; // Clear existing posts

  retweets.forEach(tweet => {
    
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
      commentText.textContent = '12 Comments';
      commentIcon.appendChild(commentText);

      commentIcon.addEventListener('click', async function() {
        localStorage.setItem('TweetType-Tweetdetails','Retweet')
        localStorage.setItem('TweetId-Tweetdetails',tweet.retweetId)
        window.open("TweetDetails.html", "_blank");
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
      likeText.textContent = `${tweet.retweetLikesCount} Likes`;
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

      document.querySelectorAll('.highlight').forEach(element => {
        element.addEventListener('click', function() {
            handleMentionClick(element.textContent);
        });
    });
  });

  tweets.forEach(tweet => {
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
    commentIcon.style.cursor = 'pointer'
    commentIcon.className = 'far fa-comment';
    const commentText = document.createElement('h6');
    commentText.style.fontSize = '8px';
    commentText.textContent = '12 Comments';
    commentIcon.appendChild(commentText);

    commentIcon.addEventListener('click', async function() {
      localStorage.setItem('TweetType-Tweetdetails','Tweet')
      localStorage.setItem('TweetId-Tweetdetails',tweet.tweetId)
      window.open("TweetDetails.html", "_blank");
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
    likeText.textContent = `${tweet.tweetLikesCount} Likes`;
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

    document.querySelectorAll('.highlight').forEach(element => {
      element.addEventListener('click', function() {
          handleMentionClick(element.textContent);
      });
  });
});
}





  



