document.addEventListener('DOMContentLoaded', () => {
    // Récupérer les informations de l'utilisateur connecté
    fetch('/api/utilisateur/current', {
        credentials: 'same-origin'
    })
        .then(response => {
            if (!response.ok) {
                console.log('Redirection vers la page de connexion : ', response.statusText);
                window.location.href = '/view/wwwroot/login.html';
                return;
            }
            return response.json();
        })
        .then(data => {
            if (data) {
                console.log('Données de l\'utilisateur : ', data);
                document.getElementById('username').textContent = data.username;
                if (data.photo1_data != null) {
                    let photoSourceData = 'data:image/png;base64,' + data.photo1_data;
                    document.getElementById('profile-photo').setAttribute("src", photoSourceData);
                }
            }
        })
        .catch(error => {
            console.error('Erreur lors de la récupération de l\'utilisateur :', error);
            window.location.href = '/view/wwwroot/login.html';
        });

    // Charger les profils publics
    fetch('/api/recherchePart/getListeProfilsPublics', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
    })
        .then(response => response.json())
        .then(data => {
            const resultsContainer = document.getElementById('results');
            resultsContainer.innerHTML = '';
            let buf = [];
            data.forEach((ele, index) => {
                if (index % 4 === 0) {
                    buf.push('<div class="container2">');
                }
                buf.push(`
                <div class="column">
                    <h2> ${ele.username} </h2>
                    <img src="data:image/png;base64,${ele.photo1_data}" width="175" height="225">
                    <p> Prénom: ${ele.prenom}</p>
                    <p> Âge: ${ele.age}</p>
                    <p>${ele.genre}</p>
                    <p>${ele.orientationS}</p>
                </div>
            `);
                if (index % 4 === 3) {
                    buf.push('</div>');
                }
            });
            if (data.length % 4 !== 0) {
                buf.push('</div>');
            }
            resultsContainer.innerHTML = buf.join('');
        })
        .catch(error => {
            console.error('Erreur lors de la récupération des profils publics :', error);
        });
});
