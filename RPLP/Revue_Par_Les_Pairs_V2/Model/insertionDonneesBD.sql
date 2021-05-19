use rplp;

insert into professeur (Nom, Prenom)
values ('Quentin', 'Nathan');

insert into cours (Nom, Professeur_id)
values ('DSED', 1);

insert into travail (Nom, DateDeRemise, Cours_id, NombresDeRevues)
values ('devoirAsp', '2021-06-05', 1, 3);

insert into etudiant (Etudiant_id, Nom, Prenom)
values ('199556@csfoy.ca', 'Tremblay', 'Jean-Sebastien');
insert into etudiant (Etudiant_id, Nom, Prenom)
values ('1994391@csfoy.ca', 'Maclellan', 'Dustin');
insert into etudiant (Etudiant_id, Nom, Prenom)
values ('1995717@csfoy.ca', 'Brousseau', 'Keven');
insert into etudiant (Etudiant_id, Nom, Prenom)
values ('1995123@csfoy.ca', 'lka', 'Paul');
insert into etudiant (Etudiant_id, Nom, Prenom)
values ('1995564@csfoy.ca', 'lkass', 'Guy');

insert into inscription (Etudiant_id, Cours_id)
values ('199556@csfoy.ca', 1);
insert into inscription (Etudiant_id, Cours_id)
values ('1994391@csfoy.ca', 1);
insert into inscription (Etudiant_id, Cours_id)
values ('1995717@csfoy.ca', 1);
insert into inscription (Etudiant_id, Cours_id)
values ('1995123@csfoy.ca', 1);
insert into inscription (Etudiant_id, Cours_id)
values ('1995564@csfoy.ca', 1);

insert into solution (Liens, Travail_id)
values ('wwwroot/temp/devoirASP', 1);

-- select 
select * from etudiant;
select * from travail;
select * from professeur;
select * from cours;
select * from solution;
select * from commentaire;
select * from inscription;
select * from correction;


-- delete 
delete from etudiant;
delete from travail;
delete from cours;
delete from solution;
delete from professeur;
delete from correction;
delete from inscription;
