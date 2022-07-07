var express = require('express')
var app = express();
const handlebars = require('express-handlebars');
const bodyParser = require('body-parser')
const nodemailer = require('nodemailer');




app.use(bodyParser.urlencoded({extended:true}))
app.use(bodyParser.json())
app.use(express.static('public'));
app.engine('handlebars', handlebars({defaultLayout:'main'}))
app.set('view engine', 'handlebars')
var cors = require('cors')
app.use(express.json())




app.get('/', async function(req, res){
  
  res.render('index', res);
});


app.get('/liberarCatraca', async function(req, res){
  res.render('libera', res);
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
 
});


app.listen(3000);
console.log("Port 3000")




function execSQLQuery(sqlQry) {

  console.log(sqlQry)
	conn.query(sqlQry, function (error, results, fields) {

	});
}

function queryJSON(sqlQry, res){
 
 
  conn.query(sqlQry, function(error, results, fields){
      if(error) 
        res.json(error);
      else
        res.json(results);
     // conn.end();
     // console.log('executou!');
  });
}