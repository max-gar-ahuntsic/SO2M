document.addEventListener('DOMContentLoaded', () => {
    const expéditeurId = 1; // Remplacez par l'ID de l'utilisateur connecté
    const destinataireId = 2; // Remplacez par l'ID de l'utilisateur cible
    const conversation = document.getElementById('conversation');
    const messageContenu = document.getElementById('message-contenu');
    const envoyerButton = document.getElementById('envoyer-button');

    function chargerMessages() {
        fetch(`/api/message/conversation/${expéditeurId}/${destinataireId}`)
            .then(response => response.json())
            .then(messages => {
                conversation.innerHTML = '';
                messages.forEach(message => {
                    const messageElement = document.createElement('div');
                    messageElement.textContent = `${message.expéditeurId === expéditeurId ? 'Vous' : 'L\'autre utilisateur'}: ${message.contenu}`;
                    conversation.appendChild(messageElement);
                });
            });
    }

    envoyerButton.addEventListener('click', () => {
        const contenu = messageContenu.value;
        if (contenu) {
            fetch(`/api/message/envoyer`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ expéditeurId, destinataireId, contenu })
            })
                .then(response => response.json())
                .then(() => {
                    messageContenu.value = '';
                    chargerMessages();
                });
        }
    });

    chargerMessages();
    setInterval(chargerMessages, 5000); // Recharge les messages toutes les 5 secondes
});
