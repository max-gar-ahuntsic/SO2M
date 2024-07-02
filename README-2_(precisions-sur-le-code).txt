
M. S�guin,

Nous sommes sensibles � l'effet de vous soumettre un code qui soit utile et propre (et donc exempt de code inutilis�);  cependant, � cause de notre utilisation d'Entity Framework, il nous faut �tre prudent car il y a une interd�pendance entre les Tables, le dataContext et les Classes.  Un changement � l'un de ces 3 �l�ments pourrait cr�er un bug et nous forcer � faire une Migration, ce qui comporte �galement son lot d'impr�vus.

Par ailleurs, bien qu'inutilis�es, les Tables Axe, ModelePsycologiques et Photos sont pertinentes � notre projet puisque dans une phase ult�rieure du d�veloppement, elles pourraient �tre utilis�es comme pr�vu initialement (Ces donn�es ont �t� incluses dans la Table Utilisateur afin de tenter d'acc�l�rer le d�veloppement de l'application.)

Merci de votre compr�hension.  Nous esp�rons que dans son ensemble le code lisible lisible, bien incr�ment� et que des noms significatifs vous en facilite la lecture.

