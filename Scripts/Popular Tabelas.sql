use GerenciadorUsuario


insert into PapelUsuario 
values('ADMINISTRADOR'), ('PADRÃO'), ('SUPERVISOR'), ('CLIENTE');

insert into StatusUsuario 
values('INATIVO TEMPORÁRIO'), ('INATIVO'), ('BLOQUEADO'), ('EM APROVAÇÃO');

SELECT * FROM PapelUsuario
SELECT * FROM StatusUsuario