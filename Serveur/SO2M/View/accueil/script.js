document.addEventListener('DOMContentLoaded', () => {
    // Récupérer les informations de l'utilisateur connecté
    fetch('/api/utilisateur/current', {
        credentials: 'same-origin'
    })
        .then(response => {
            if (!response.ok) {
                window.location.href = '/view/wwwroot/login.html';
                return;
            }
            return response.json();
        })
        .then(data => {
            document.getElementById('username').textContent = data.Username;
            document.getElementById('profile-photo').src = data.ProfilePhotoURL;
            loadPosts();
        })
        .catch(error => {
            window.location.href = '/view/wwwroot/login.html';
        });
    // Charger les posts
    function loadPosts() {
        fetch('/api/post/posts', {
            credentials: 'same-origin'
        })
            .then(response => response.json())
            .then(posts => {
                const postsContainer = document.getElementById('posts');
                postsContainer.innerHTML = '';
                posts.forEach(post => {
                    const postElement = document.createElement('div');
                    postElement.className = 'post';
                    postElement.innerHTML = `
                        <strong>${post.username}</strong>
                        <p>${post.content}</p>
                        <button class="btn-supp" onclick="deletePost(${post.id})">Supprimer</button>
                    `;
                    postsContainer.appendChild(postElement);
                });
            })
            .catch(error => console.error('Erreur lors du chargement des posts:', error));
    }

    // Publier un nouveau statut
    document.getElementById('post-button').addEventListener('click', () => {
        const content = document.getElementById('post-content').value;
        if (content) {
            fetch('/api/post/post', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: `content=${encodeURIComponent(content)}`,
                credentials: 'same-origin'
            })
                .then(response => response.json())
                .then(post => {
                    const postsContainer = document.getElementById('posts');
                    const postElement = document.createElement('div');
                    postElement.className = 'post';
                    postElement.innerHTML = `
                        <strong>${post.username}</strong>
                        <p>${post.content}</p>
                        <button class="btn-supp" onclick="deletePost(${post.id})">Supprimer</button>
                    `;
                    postsContainer.insertBefore(postElement, postsContainer.firstChild);
                    document.getElementById('post-content').value = '';
                })
                .catch(error => console.error('Erreur lors de la publication du post:', error));
        }
    });

    // Supprimer un post
    window.deletePost = function (postId) {
        fetch(`/api/post/post/${postId}`, {
            method: 'DELETE',
            credentials: 'same-origin'
        })
            .then(response => {
                if (response.ok) {
                    loadPosts();
                } else {
                    console.error('Erreur lors de la suppression du post:', response.statusText);
                }
            })
            .catch(error => console.error('Erreur lors de la suppression du post:', error));
    }

    // Gestion de la déconnexion
    document.getElementById('logout-button').addEventListener('click', () => {
        fetch('/api/utilisateur/logout', {
            method: 'POST',
            credentials: 'same-origin'
        })
            .then(response => {
                console.log('Réponse reçue pour la déconnexion:', response);
                if (!response.ok) {
                    throw new Error(`Network response was not ok: ${response.statusText}`);
                }
                window.location.href = '/view/wwwroot/login.html'; // Redirigez vers la page de connexion après la déconnexion
            })
            .catch(error => {
                console.error('Erreur lors de la déconnexion:', error);
            });
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
