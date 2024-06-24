24-06-24---

1)_ Doit exécuter script du 24-06-24 pour fonctionner -- sinon brisé.

2)_ Pour ajouter une photo dans une page html, voir la page Questionnaire.html:
 
           //----insertion photo 
          let photoSourceDate = 'data:image/png;base64,' + data.photo1_data + '';
          document.getElementById('profile-photo').setAttribute("src", photoSourceDate);
