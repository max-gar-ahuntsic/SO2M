function loadUserInfo() {
    console.log("Loading user info..."); // Pour vérifier que le script est exécuté
    fetch('http://localhost:5160/api/utilisateur/current') // Assurez-vous que l'URL est correcte
        .then(response => {
            if (!response.ok) {
                console.error('Failed to fetch user info:', response.status);
                window.location.href = '/FrontEnd/wwwroot/login.html';
                return null;
            }
            return response.json();
        })
        .then(data => {
            console.log("User data:", data); // Pour voir les données récupérées
            if (data) {
                document.getElementById('username').textContent = data.username;
                document.getElementById('profile-photo').src = data.profilePhotoURL;
                console.log("User info displayed."); // Pour confirmer que les données sont insérées dans le DOM
            } else {
                console.error('No user data received.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            window.location.href = '/FrontEnd/wwwroot/login.html';
        });
}

document.addEventListener('DOMContentLoaded', loadUserInfo);
