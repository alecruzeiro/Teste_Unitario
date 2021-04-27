# Processadora de cart�o de cr�dito

## Solicitar Cart�o de Cr�dito
	Eu como cliente desejo solicitar um cart�o de acordo com a minha renda
		1 - Ao solicitar um cart�o, deve ser informado Nome, CPF, RG, Profiss�o, renda e nome escolhido no cart�o
			1.1 - Se o nome for vazio, uma mensagem dever� retornar "Insira um nome v�lido"
			1.2 - Se o cpf for inv�lido ou vazio, uma mensagem dever� retornar "Insira um cpf v�lido"
			1.3 - Se o RG for vazio, uma mensagem dever� retornar "Insira um RG v�lido"
			1.4 - Se a profiss�o for vazia, uma mensagem dever� retornar "Insira uma profiss�o v�lida"
			1.5 - Se a renda for vazia, uma mensagem dever� retornar "Insira uma renda v�lida"
			1.6 - Caso a renda seja menor que R$:800,00, dever� retornar "Renda deve ser maior que R$:800,00"  
			1.7 - Caso o cpf j� esteja cadastrado na base dever� retornar "Cpf j� cadastrado"  
		2 - Caso a renda seja maior que R$:800,00 e menor que R$:2500,00, um cart�o do tipo Gold dever� ser ofertado
		3 - Caso a renda seja maior que R$:2500,00, ambos os cart�es dever�o ser ofertados
		4 - Quando cadastrado com sucesso, o pedido de cart�o dever� ser enviado � mesa de cr�dito online
		5 - Ao ter o pedido aprovado, um n�mero de cart�o virtual, CVV, data de validade e nome no cart�o dever� ser gerado e exibido ao cliente
		6 - A data de validade � composta por m�s e ano e apresentada mm/aa

## Transacionar com Cart�o de Cr�dito Virtual
	Eu como cliente desejo fazer uma transa��o com meu cart�o de cr�dito virtual
		1 - Ao fazer uma transa��o em lojas virtuais, deve ser informado Nome no cart�o, N�mero do cart�o virtual, CVV, data de validade, CPF e valor total da transa��o
			1.1 - Se o n�mero do cart�o for inv�lido, uma mensagem dever� retornar "N�mero do cart�o inv�lido"
			1.2 - Se o saldo dispon�vel do cart�o de cr�dito for menor que o valor total do pedido, uma mensagem dever� retornar "Saldo insuficiente"
			1.3 - Se o cvv do cart�o for inv�lido, uma mensagem dever� retornar "N�mero do cvv inv�lido"
			1.4 - Se a data de validade do cart�o for inv�lida, uma mensagem dever� retornar "Data de validade inv�lida"
			1.5 - Se o m�s da data de validade e o ano da data de validade forem menores que o m�s e ano atual, uma mensagem dever� retornar "cart�o vencido"
			1.6 - Se o status do cart�o estiver diferente de ativo, uma mensagem dever� retornar "Cart�o inv�lido"
		2 - Caso qualquer problema seja encontrado, al�m da mensagem de erro dever� retornar o status de compra negada
		3 - Apenas uma mensagem de erro dever� ser retornada por tentativa
		4 - Ap�s tr�s tentativas mal sucedidas consecutivas, o cart�o virtual dever� ter seu status alterado para bloqueado
		5 - Caso tudo seja validado com sucesso, a compra dever� retornar o status de compra aprovada