# Processadora de cartão de crédito

## Solicitar Cartão de Crédito
	Eu como cliente desejo solicitar um cartão de acordo com a minha renda
		1 - Ao solicitar um cartão, deve ser informado Nome, CPF, RG, Profissão, renda e nome escolhido no cartão
			1.1 - Se o nome for vazio, uma mensagem deverá retornar "Insira um nome válido"
			1.2 - Se o cpf for inválido ou vazio, uma mensagem deverá retornar "Insira um cpf válido"
			1.3 - Se o RG for vazio, uma mensagem deverá retornar "Insira um RG válido"
			1.4 - Se a profissão for vazia, uma mensagem deverá retornar "Insira uma profissão válida"
			1.5 - Se a renda for vazia, uma mensagem deverá retornar "Insira uma renda válida"
			1.6 - Caso a renda seja menor que R$:800,00, deverá retornar "Renda deve ser maior que R$:800,00"  
			1.7 - Caso o cpf já esteja cadastrado na base deverá retornar "Cpf já cadastrado"  
		2 - Caso a renda seja maior que R$:800,00 e menor que R$:2500,00, um cartão do tipo Gold deverá ser ofertado
		3 - Caso a renda seja maior que R$:2500,00, ambos os cartões deverão ser ofertados
		4 - Quando cadastrado com sucesso, o pedido de cartão deverá ser enviado à mesa de crédito online
		5 - Ao ter o pedido aprovado, um número de cartão virtual, CVV, data de validade e nome no cartão deverá ser gerado e exibido ao cliente
		6 - A data de validade é composta por mês e ano e apresentada mm/aa

## Transacionar com Cartão de Crédito Virtual
	Eu como cliente desejo fazer uma transação com meu cartão de crédito virtual
		1 - Ao fazer uma transação em lojas virtuais, deve ser informado Nome no cartão, Número do cartão virtual, CVV, data de validade, CPF e valor total da transação
			1.1 - Se o número do cartão for inválido, uma mensagem deverá retornar "Número do cartão inválido"
			1.2 - Se o saldo disponível do cartão de crédito for menor que o valor total do pedido, uma mensagem deverá retornar "Saldo insuficiente"
			1.3 - Se o cvv do cartão for inválido, uma mensagem deverá retornar "Número do cvv inválido"
			1.4 - Se a data de validade do cartão for inválida, uma mensagem deverá retornar "Data de validade inválida"
			1.5 - Se o mês da data de validade e o ano da data de validade forem menores que o mês e ano atual, uma mensagem deverá retornar "cartão vencido"
			1.6 - Se o status do cartão estiver diferente de ativo, uma mensagem deverá retornar "Cartão inválido"
		2 - Caso qualquer problema seja encontrado, além da mensagem de erro deverá retornar o status de compra negada
		3 - Apenas uma mensagem de erro deverá ser retornada por tentativa
		4 - Após três tentativas mal sucedidas consecutivas, o cartão virtual deverá ter seu status alterado para bloqueado
		5 - Caso tudo seja validado com sucesso, a compra deverá retornar o status de compra aprovada