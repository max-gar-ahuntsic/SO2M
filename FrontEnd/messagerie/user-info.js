function loadUserInfo() {
    fetch('http://localhost:5160/api/utilisateur/current')
        .then(response => {
            if (!response.ok) {
                console.error('Utilisateur non connecté ou erreur de récupération des données.');
                window.location.href = '/FrontEnd/wwwroot/login.html';
                return null;
            }
            return response.json();
        })
        .then(data => {
            if (data) {
                document.getElementById('username').textContent = data.Username;
                document.getElementById('profile-photo').src = data.ProfilePhotoURL;
            } else {
                console.error('Aucune donnée utilisateur reçue.');
            }
        })
        .catch(error => {
            console.error('Erreur lors de la récupération des données de l\'utilisateur:', error);
            window.location.href = '/FrontEnd/wwwroot/login.html';
        });
}

document.addEventListener('DOMContentLoaded', loadUserInfo);
