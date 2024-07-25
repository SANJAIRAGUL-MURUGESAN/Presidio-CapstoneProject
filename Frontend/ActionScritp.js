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
        alert('You can only upload a maximum of 4 images');
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

    if (uploadedFiles.length < 1 || uploadedFiles.length > 3) {
        alert('Please upload between 1 and 3 images.');
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
              "userId": 1,
              "tweetContent": postContent,
              "isCommentEnable": "yes",
              "repostTweetId": 0,
              "tweetHashtags": comments,
              "tweetMentions": mentions
          })
      })
      .then(async res => {
          const data = await res.json();
          if (!res.ok) {
              console.log(data.errorCode)
          }else{
              console.log(data)
              // alert('Hey User, Your Feed Added Successfully!');
              await addFeedImages();
          }
      })
      .catch(error => {
          alert(error);
      });
  }

  async function addFeedImages(){
        fetch('https://localhost:7186/api/Tweet/AddTweet', {
            method: 'POST',
            body: formData
        })
        .then(data => {
            console.log(data);
            alert('Images uploaded successfully.');
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

  addFeedContent()

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


  



