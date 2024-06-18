const chatInput = document.querySelector("#message-contenu");
const sendChatBtn = document.querySelector("#envoyer-button");
const conversation = document.querySelector("#conversation");

let userMessage;
const API_KEY = "0m8PaV7y3rTvBEgWxHtcT3BlbkFJkFf4VwhV9qkl0tYEqXcv";  // Remplacez par votre clé API
const inputInitHeight = chatInput.scrollHeight;

const createChatDiv = (message, className) => {
    const chatDiv = document.createElement("div");
    chatDiv.classList.add("chat", className);
    let chatContent = '';
    if (className === "outgoing") {
        chatContent = `<div class="chat-content"><p></p></div><span class="user-icon">😊</span>`;
    } else {
        chatContent = `<span class="bot-icon">🤖</span><div class="chat-content"><p></p></div>`;
    }
    chatDiv.innerHTML = chatContent;
    chatDiv.querySelector("p").textContent = message;
    return chatDiv;
}

const generateReponse = (incomingChatDiv) => {
    const API_URL = "https://api.openai.com/v1/chat/completions";
    const messageElement = incomingChatDiv.querySelector("p");

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
            }, { role: "user", content: userMessage }]
        })
    }

    fetch(API_URL, requestOptions)
        .then(res => res.json())
        .then(data => {
            if (data.choices && data.choices[0] && data.choices[0].message && data.choices[0].message.content) {
                messageElement.textContent = data.choices[0].message.content;
            } else {
                throw new Error("Invalid response format");
            }
        })
        .catch((err) => {
            console.error("Erreur de requête API:", err); // Log l'erreur dans la console
            messageElement.classList.add("error");
            messageElement.textContent = "Oups! Ca n'a pas fonctionné. Essayé à nouveau.";
        })
        .finally(() => conversation.scrollTo(0, conversation.scrollHeight));
};

const handleChat = () => {
    userMessage = chatInput.value.trim();
    if (!userMessage) return;
    chatInput.value = "";
    chatInput.style.height = `${inputInitHeight}px`;

    conversation.appendChild(createChatDiv(userMessage, "outgoing"));
    conversation.scrollTo(0, conversation.scrollHeight);

    setTimeout(() => {
        const incomingChatDiv = createChatDiv("En attente...", "incoming");
        conversation.appendChild(incomingChatDiv);
        conversation.scrollTo(0, conversation.scrollHeight);
        generateReponse(incomingChatDiv);
    }, 600);
}

chatInput.addEventListener("input", () => {
    chatInput.style.height = `${inputInitHeight}px`;
    chatInput.style.height = `${chatInput.scrollHeight}px`;
});

chatInput.addEventListener("keydown", (e) => {
    if (e.key === "Enter" && !e.shiftKey) {
        e.preventDefault();
        handleChat();
    }
});

sendChatBtn.addEventListener("click", handleChat);
