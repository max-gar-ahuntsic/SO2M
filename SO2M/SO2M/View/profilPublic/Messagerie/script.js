const chatInput = document.querySelector(".chat-input textarea");
const sendChatBtn = document.querySelector(".chat-input span");
const chatbox = document.querySelector(".chatbox");
const chatboiteToggler = document.querySelector(".chatboite-toggler");
const chatboiteCloseBtn = document.querySelector(".close-btn");

let userMessage;
const API_KEY = "sk-proj-0m8PaV7y3rTvBEgWxHtcT3BlbkFJkFf4VwhV9qkl0tYEqXcv";
const inputInitHeight = chatInput.scrollHeight;

const createChatLi = (message, className) => {
  //Création de une liste de chat <li> avec la transmition des paramétres message et classMessage
  const chatLi = document.createElement("li");
  chatLi.classList.add("chat", className);
  let chatContent = className === "outgoing" ? `<p></p>` : `<span class="material-symbols-outlined">smart_toy</span><p></p>`;
  chatLi.innerHTML = chatContent;
  chatLi.querySelector("p").textContent = message;
  return chatLi;
}

const generateReponse = (incomingChatLi) => {
    const API_URL = "https://api.openai.com/v1/chat/completions";
    const messageElement = incomingChatLi.querySelector("p");

    // Definir les propietés et le messages pour API request
    const requestOptions = {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${API_KEY}`
        },
        body: JSON.stringify({
            model: "gpt-3.5-turbo-16k",
            messages: [{
                "role": "system",
                "content": "You are a helpful assistant."
              },{role: "user", content: userMessage}]
        })
    }
    //Envoyer POST request to API, get response
    fetch(API_URL, requestOptions).then(res => res.json()).then(data =>
        {
            messageElement.textContent = data.choices[0].message.content;
        }).catch((err) => {
            messageElement.classList.add("error");
            messageElement.textContent = "Oups! Ca n'a pas fonctionné. Essayé à nouveau.";
        }).finally(() => chatbox.scrollTo(0, chatbox.scrollHeight));
};

const handleChat = () => {
  userMessage = chatInput.value.trim();
  if(!userMessage) return;
  chatInput.value = "";
  chatInput.style.height = `${inputInitHeight}px`;
  
  // Append the user's message to the chatbox
  chatbox.appendChild (createChatLi(userMessage, "outgoing"));
  chatbox.scrollTo(0, chatbox.scrollHeight);

  setTimeout(() => {
    // Montrer message En attente pour signifier le temps d'une réponse
    const incomingChatLi = createChatLi("En attente...", "incoming");
    chatbox.appendChild (incomingChatLi);
    chatbox.scrollTo(0, chatbox.scrollHeight);
    generateReponse(incomingChatLi);
  }, 600);
}

chatInput.addEventListener("input", () =>{
  // On ajoute l'hauter des input textarea basé sur leur container
  chatInput.style.height = `${inputInitHeight}px`;
  chatInput.style.height = `${chatInput.scrollHeight}px`;
});

chatInput.addEventListener("keydown", (e) =>{
  /* If Enter key is pressed with out Shift key and
  the window width is greater than 800px, handler the chat
   */
  if (e.key === "Enter" && !e.shiftKey && windows.innerWithd > 800) {
    e.preventDefault();
    handleChat();
  }
});

sendChatBtn.addEventListener("click", handleChat);
chatboiteCloseBtn.addEventListener("click", () => document.body.classList.remove("show-chatboite"));
chatboiteToggler.addEventListener("click", () => document.body.classList.toggle("show-chatboite"));