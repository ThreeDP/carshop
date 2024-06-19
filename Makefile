IMAGES				=			nginx postgres back-image front-image
PROCESS				=			db proxy frontend backend
VOLUMES				=			db-volume images-volume 
NETWORKS			=			controller_net view_net

all: create_dirs build up
	docker compose up -d

dev:
	docker compose up -d
	cd backend/carshop/CarShop && dotnet watch

front:
	cd frontend/CarShopView && dotnet watch

build:
	docker compose build

up:
	docker compose up -d

down:
	docker compose down

create_dirs:
	mkdir -p storage
	mkdir -p storage/database
	mkdir -p storage/images

clean: down
	docker rm -f $(PROCESS)

fclean: clean
	docker rmi -f $(IMAGES)
	docker volume rm $(VOLUMES)
	docker network rm $(NETWORKS)

re: fclean all