use GerenciadorUsuario


insert into PapelUsuario 
values('ADMINISTRADOR'), ('PADR�O'), ('SUPERVISOR'), ('CLIENTE');

insert into StatusUsuario 
values('INATIVO TEMPOR�RIO'), ('INATIVO'), ('BLOQUEADO'), ('EM APROVA��O');

SELECT * FROM PapelUsuario
SELECT * FROM StatusUsuario