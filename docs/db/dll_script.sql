-- public.activitiy_types определение

-- Drop table

-- DROP TABLE public.activitiy_types;

CREATE TABLE public.activitiy_types (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT activitiy_types_pk PRIMARY KEY (id),
	CONSTRAINT activitiy_types_unique UNIQUE (name)
);


-- public.cities определение

-- Drop table

-- DROP TABLE public.cities;

CREATE TABLE public.cities (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT cities_pk PRIMARY KEY (id),
	CONSTRAINT cities_unique_name UNIQUE (name)
);


-- public.countries определение

-- Drop table

-- DROP TABLE public.countries;

CREATE TABLE public.countries (
	id serial4 NOT NULL,
	name_ru varchar NOT NULL,
	name_eng varchar NOT NULL,
	literal_code varchar NOT NULL,
	numeric_code int4 NOT NULL,
	CONSTRAINT countries_pk PRIMARY KEY (id),
	CONSTRAINT countries_unique_literal UNIQUE (literal_code),
	CONSTRAINT countries_unique_name_eng UNIQUE (name_eng),
	CONSTRAINT countries_unique_name_ru UNIQUE (name_ru),
	CONSTRAINT countries_unique_numeric UNIQUE (numeric_code)
);


-- public.direction_types определение

-- Drop table

-- DROP TABLE public.direction_types;

CREATE TABLE public.direction_types (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT direction_types_pk PRIMARY KEY (id),
	CONSTRAINT direction_types_unique UNIQUE (name)
);


-- public.genders определение

-- Drop table

-- DROP TABLE public.genders;

CREATE TABLE public.genders (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT genders_pk PRIMARY KEY (id),
	CONSTRAINT genders_unique UNIQUE (name)
);


-- public.roles определение

-- Drop table

-- DROP TABLE public.roles;

CREATE TABLE public.roles (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT roles_pk PRIMARY KEY (id),
	CONSTRAINT roles_unique UNIQUE (name)
);


-- public.city_emblems определение

-- Drop table

-- DROP TABLE public.city_emblems;

CREATE TABLE public.city_emblems (
	id serial4 NOT NULL,
	city serial4 NOT NULL,
	emblem varchar NOT NULL,
	CONSTRAINT city_emblems_pk PRIMARY KEY (id),
	CONSTRAINT city_emblems_unique_image UNIQUE (emblem),
	CONSTRAINT city_emblems_cities_fk FOREIGN KEY (city) REFERENCES public.cities(id) ON DELETE CASCADE ON UPDATE CASCADE
);


-- public.users определение

-- Drop table

-- DROP TABLE public.users;

CREATE TABLE public.users (
	id int4 DEFAULT nextval('participants_id_seq'::regclass) NOT NULL,
	full_name varchar NOT NULL,
	mail varchar NOT NULL,
	date_of_birth timestamp NOT NULL,
	country int4 DEFAULT nextval('participants_country_seq'::regclass) NOT NULL,
	phone_number varchar NOT NULL,
	"password" varchar NOT NULL,
	image varchar NULL,
	gender int4 DEFAULT nextval('participants_gender_seq'::regclass) NOT NULL,
	"role" int4 NOT NULL,
	CONSTRAINT participants_pk PRIMARY KEY (id),
	CONSTRAINT participants_unique_mail UNIQUE (mail),
	CONSTRAINT participants_countries_fk FOREIGN KEY (country) REFERENCES public.countries(id) ON DELETE RESTRICT ON UPDATE CASCADE,
	CONSTRAINT participants_genders_fk FOREIGN KEY (gender) REFERENCES public.genders(id) ON DELETE RESTRICT ON UPDATE CASCADE,
	CONSTRAINT users_roles_fk FOREIGN KEY ("role") REFERENCES public.roles(id) ON DELETE RESTRICT ON UPDATE CASCADE
);


-- public.activities определение

-- Drop table

-- DROP TABLE public.activities;

CREATE TABLE public.activities (
	id serial4 NOT NULL,
	description varchar NOT NULL,
	"date" timestamp NOT NULL,
	duration_in_days int4 NOT NULL,
	city serial4 NOT NULL,
	activity_type serial4 NOT NULL,
	winner int4 NULL,
	CONSTRAINT activities_pk PRIMARY KEY (id),
	CONSTRAINT activities_activitiy_types_fk FOREIGN KEY (activity_type) REFERENCES public.activitiy_types(id) ON DELETE RESTRICT ON UPDATE CASCADE,
	CONSTRAINT activities_cities_fk FOREIGN KEY (city) REFERENCES public.cities(id) ON DELETE RESTRICT ON UPDATE CASCADE,
	CONSTRAINT activities_users_fk FOREIGN KEY (winner) REFERENCES public.users(id) ON DELETE RESTRICT ON UPDATE CASCADE
);


-- public.jurors определение

-- Drop table

-- DROP TABLE public.jurors;

CREATE TABLE public.jurors (
	"user" int4 DEFAULT nextval('jury_user_seq'::regclass) NOT NULL,
	direction int4 DEFAULT nextval('jury_direction_seq'::regclass) NOT NULL,
	CONSTRAINT jury_pk PRIMARY KEY ("user"),
	CONSTRAINT jury_direction_types_fk FOREIGN KEY (direction) REFERENCES public.direction_types(id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT jury_users_fk FOREIGN KEY ("user") REFERENCES public.users(id) ON DELETE CASCADE ON UPDATE CASCADE
);


-- public.moderators определение

-- Drop table

-- DROP TABLE public.moderators;

CREATE TABLE public.moderators (
	"user" serial4 NOT NULL,
	direction_type serial4 NOT NULL,
	activity_type serial4 NOT NULL,
	CONSTRAINT moderators_pk PRIMARY KEY ("user"),
	CONSTRAINT moderators_activitiy_types_fk FOREIGN KEY (activity_type) REFERENCES public.activitiy_types(id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT moderators_direction_types_fk FOREIGN KEY (direction_type) REFERENCES public.direction_types(id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT moderators_users_fk FOREIGN KEY ("user") REFERENCES public.users(id) ON DELETE CASCADE ON UPDATE CASCADE
);


-- public.events определение

-- Drop table

-- DROP TABLE public.events;

CREATE TABLE public.events (
	id serial4 NOT NULL,
	activity serial4 NOT NULL,
	description varchar NOT NULL,
	day_number int4 NOT NULL,
	start_time timestamp NOT NULL,
	moderator serial4 NOT NULL,
	CONSTRAINT events_pk PRIMARY KEY (id),
	CONSTRAINT events_activities_fk FOREIGN KEY (activity) REFERENCES public.activities(id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT events_moderators_fk FOREIGN KEY (moderator) REFERENCES public.moderators("user") ON DELETE RESTRICT ON UPDATE CASCADE
);


-- public.events_jurors определение

-- Drop table

-- DROP TABLE public.events_jurors;

CREATE TABLE public.events_jurors (
	"event" serial4 NOT NULL,
	jury serial4 NOT NULL,
	CONSTRAINT events_jurors_pk PRIMARY KEY (event, jury),
	CONSTRAINT events_jurors_events_fk FOREIGN KEY ("event") REFERENCES public.events(id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT events_jurors_jury_fk FOREIGN KEY (jury) REFERENCES public.jurors("user") ON DELETE RESTRICT ON UPDATE CASCADE
);