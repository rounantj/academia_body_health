var express = require('express');

var app = express();
const handlebars = require('express-handlebars');
const bodyParser = require('body-parser')
const nodemailer = require('nodemailer');
var op = require('./db')


const mysql      = require('mysql');
const conn = mysql.createConnection({
  host     : "localhost",
  port     : 3306,
  user     : "root",
  password : "",
  database : "fingers"
});

var clientes = require('./clientes')
var times = require('./times')



app.use(bodyParser.urlencoded({extended:true}))
app.use(bodyParser.json())
app.use(express.static('public'));
app.engine('handlebars', handlebars({defaultLayout:'main'}))
app.set('view engine', 'handlebars')
var cors = require('cors')
app.use(express.json())


/*
app.use((req, res, next) => {
	//Qual site tem permissão de realizar a conexão, no exemplo abaixo está o "*" indicando que qualquer site pode fazer a conexão
    res.header("Access-Control-Allow-Origin", "*");
	//Quais são os métodos que a conexão pode realizar na API
    res.header("Access-Control-Allow-Methods", 'GET,PUT,POST,DELETE');
    app.use(cors());
    next();
});

*/
const { Op } = require("sequelize");
var ano = new Date().getFullYear(); 
var mes = new Date().getMonth() +1;  if(mes<10){mes = "0"+mes}
var dia = new Date().getDate();  if(dia<10){dia = "0"+dia}




app.get('/instrutores', async function(req, res){ 
  var bloco  = await clientes.findAll({where:{categoria:'instrutor'}})
  var bloco2  = await clientes.findAll({where:{categoria:'aluno'}})

  res.render('instrutor', {INSTRUTORES: bloco, TOTAL_INSTRUTORES: bloco.length,NAO_INSTRUTORES: bloco2, TOTAL_NAO_INSTRUTORES: bloco2.length});  
});

app.get('/gestao', async function(req, res){
execSQLQuery("update clientes set categoria = 'aluno' where categoria is null");
execSQLQuery("update clientes set num_identificador = id")
  var dataAmanha = new Date(new Date().getTime() + 168 * 60 * 60 * 1000)
  var ano7 = dataAmanha.getFullYear(); 
  var mes7 = dataAmanha.getMonth() +1;  if(mes7<10){mes7 = "0"+mes7}
  var dia7 = dataAmanha.getDate();  if(dia7<10){dia7 = "0"+dia7}
dataAmanha = ano7+"-"+mes7+"-"+dia7
console.log(dataAmanha)

  var time = await times.findAll()
  var bloco  = await clientes.findAll({where:{dataVencimento:{[Op.gte]:ano+"-"+mes+"-"+dia},categoria:'aluno'}})
  var bloco3  = await clientes.findAll({where:{dataVencimento:{[Op.lte]:ano+"-"+mes+"-"+dia},categoria:'aluno'}})

  var vencendo  = await clientes.findAll({order:[["dataVencimento",'DESC']],where:{dataVencimento:{[Op.lte]:dataAmanha},categoria:'aluno'}})

 var bloco2 = await clientes.findAll({where:{categoria:'aluno'}},{order:[["num_identificador",'ASC']]});
 

 


  res.render('index', {VENCENDO: vencendo, TOTAL_VENCENDO: vencendo.length,CLIENTES: bloco2,TOTAL_CLIENTES: bloco2.length, CLIENTES_ATIVOS: bloco, TOTAL_ATIVOS: bloco.length, CLIENTES_INADIMPLENTES:bloco3, TOTAL_ATRASADOS: bloco3.length, HORARIOS:time});
  
});
app.get('/treinamentos', async function(req, res){
  var bloco2 = await clientes.findAll();
  res.render('qrcode', {CLIENTES: bloco2});
  
});

app.get('/insereHorario/:descricao/:comeca/:termina/:diaSemana', async function(req, res){
  
  queryJSON("insert into horarios values (null, '"+req.params.descricao+"','"+req.params.comeca+"','"+req.params.termina+"','"+req.params.diaSemana+"', now(), now())",res)
});

app.get('/removeInstrutor/:id', async function(req, res){
  
  queryJSON("update clientes set categoria = 'aluno', dataVencimento = now() where id = "+req.params.id,res)
});

app.get('/adicionaInstrutor/:id', async function(req, res){
  
  queryJSON("update clientes set categoria = 'instrutor', dataVencimento = '9999-12-30' where id = "+req.params.id,res)
});

app.get('/updateHorario/:id/:descricao/:comeca/:termina/:diaSemana', async function(req, res){
  
  queryJSON("update horarios set descricao = '"+req.params.descricao+"', comeca= '"+req.params.comeca+"', termina ='"+req.params.termina+"', horario = '"+req.params.diaSemana+"' where id = "+req.params.id,res)
});


app.get('/exclui/:id', async function(req, res){
  
  queryJSON("delete from clientes where num_identificador = "+req.params.id,res)
});




app.get('/renova/:numeroIdentificador/:data', async function(req, res){
  
  queryJSON("update clientes set dataVencimento = '"+req.params.data+"' where num_identificador = '"+req.params.numeroIdentificador+"'",res)
});
app.get('/treina/:numeroIdentificador/:treino/:tipoTreino', async function(req, res){
  
  queryJSON("update clientes set treinos = '"+req.params.treino+"', tipoTreino = '"+req.params.tipoTreino+"' where num_identificador = '"+req.params.numeroIdentificador+"'",res)
});

app.get('/pegaClientes', async function(req, res){
  
  queryJSON("select *, DATE_FORMAT(clienteStatus.dataVencimento, '%Y-%m-%d') as prazo, now() as hoje from clientes inner join clienteStatus where clientes.num_identificador = clienteStatus.num_identificacao;",res);
});


app.get('/liberarCatraca', async function(req, res){

  var net = require('net');

  var HOST = '192.168.1.105';
  var PORT = 9000;
  
  var client = new net.Socket();
  client.connect(PORT, HOST, function() {
      console.log('CONNECTED TO: ' + HOST + ':' + PORT);
     
      client.write("LIBERAR");
  });
  
  client.on('data', function(data) {
      console.log('DATA: ' + data);
      client.destroy();
  });
  
  client.on('close', function() {
      console.log('Connection closed');

  });
  res.render('libera', res);
});


app.listen(3000);
console.log("Port 3000")




function execSQLQuery(sqlQry) {

  console.log(sqlQry)
	conn.query(sqlQry, function (error, results, fields) {

	});
}

function queryJSON(sqlQry, res){
 
  console.log(sqlQry)
  conn.query(sqlQry, function(error, results, fields){
      if(error) 
        res.json(error);
      else
        res.json(results);
     // conn.end();
     // console.log('executou!');
  });
}