document.addEventListener('DOMContentLoaded', () => {
    // Récupérer les informations de l'utilisateur connecté
    fetch('/api/utilisateur/current')
        .then(response => response.json())
        .then(data => {
            document.getElementById('username').textContent = data.Username;
            document.getElementById('profile-photo').src = data.ProfilePhotoURL;
        })
        .catch(error => console.error('Error:', error));

    // Simuler des posts (en attendant l'intégration avec le backend)
    const posts = [
        { username: 'Utilisateur1', content: 'Bonjour à tous !' },
        { username: 'Utilisateur2', content: 'Quelqu\'un veut discuter ?' }
    ];

    const postsContainer = document.getElementById('posts');
    posts.forEach(post => {
        const postElement = document.createElement('div');
        postElement.className = 'post';
        postElement.innerHTML = `<strong>${post.username}</strong><p>${post.content}</p>`;
        postsContainer.appendChild(postElement);
    });

    document.getElementById('post-button').addEventListener('click', () => {
        const content = document.getElementById('post-content').value;
        if (content) {
            const postElement = document.createElement('div');
            postElement.className = 'post';
            postElement.innerHTML = `<strong>Vous</strong><p>${content}</p>`;
            postsContainer.insertBefore(postElement, postsContainer.firstChild);
            document.getElementById('post-content').value = '';
        }
    });

    // Gestion de la déconnexion
    document.getElementById('logout-button').addEventListener('click', () => {
        fetch('/api/utilisateur/logout', { method: 'POST' })
            .then(() => {
                window.location.href = '/login.html';
            })
            .catch(error => console.error('Error:', error));
    });

    // Boutons du menu en haut
    document.getElementById('message-button').addEventListener('click', () => {
        window.location.href = '/FrontEnd/messagerie/messagerie.html';
    });

    document.getElementById('search-button').addEventListener('click', () => {
        // Ajoutez ici la logique de recherche
    });

    document.getElementById('photo-button').addEventListener('click', () => {
        // Ajoutez ici la logique pour voir les photos
    });

    document.getElementById('favorites-button').addEventListener('click', () => {
        // Ajoutez ici la logique pour afficher les favoris
    });

    document.getElementById('settings-button').addEventListener('click', () => {
        document.getElementById('settings').style.display = 'block';
    });
});
