IMAGES				=			nginx postgres
PROCESS				=			mvtest-db-1
VOLUMES				=			mvtest_db-volume 

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
	docker volume rm $(VOLUMES)
	docker rm -f $(PROCESS)

fclean: clean
	docker rmi -f $(IMAGES)
