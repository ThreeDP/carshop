IMAGES				=			nginx postgres back-image front-image
PROCESS				=			db proxy frontend backend
VOLUMES				=			db-volume images-volume 
NETWORKS			=			controller_net view_net

all: build
	sudo docker compose up -d
	cd backend/carshop/Carshop; sudo dotnet watch

build:
	sudo docker compose build

create_dirs:
	mkdir -p storage
	mkdir -p storage/database
	mkdir -p storage/images

clean:
	docker stop $(PROCESS)
	docker rm -f $(PROCESS)

fclean: clean
	docker rmi -f $(IMAGES)
	docker volume rm $(VOLUMES)
	docker network rm $(NETWORKS)
