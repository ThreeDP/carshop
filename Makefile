all:
	sudo docker compose up -d
	cd carshop/CarShop/; dotnet watch