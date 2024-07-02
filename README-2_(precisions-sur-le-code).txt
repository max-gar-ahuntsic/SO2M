
M. Séguin,

Nous sommes sensibles à l'effet de vous soumettre un code qui soit utile et propre (et donc exempt de code inutilisé);  cependant, à cause de notre utilisation d'Entity Framework, il nous faut être prudent car il y a une interdépendance entre les Tables, le dataContext et les Classes.  Un changement à l'un de ces 3 éléments pourrait créer un bug et nous forcer à faire une Migration, ce qui comporte également son lot d'imprévus.

Par ailleurs, bien qu'inutilisées, les Tables Axe, ModelePsycologiques et Photos sont pertinentes à notre projet puisque dans une phase ultérieure du développement, elles pourraient être utilisées comme prévu initialement (Ces données ont été incluses dans la Table Utilisateur afin de tenter d'accélérer le développement de l'application.)

Merci de votre compréhension.  Nous espérons que dans son ensemble le code lisible lisible, bien incrémenté et que des noms significatifs vous en facilite la lecture.

