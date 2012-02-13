/*
* Script to initialize database, schemas and associated tables
* in correct sequence.
* Author:	Markus Vetsch
* Date:		20120213
*/

-- Create schema do

CREATE SCHEMA "do"
  AUTHORIZATION admin;

-- Create Table: "do".player

CREATE TABLE "do".player
(
  id bigint NOT NULL,
  firstname character varying(30) NOT NULL,
  lastname character varying(30) NOT NULL,
  birthdate date NOT NULL,
  country character varying(2),
  CONSTRAINT pk_player PRIMARY KEY (id )
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE "do".player
  OWNER TO admin;

-- Create Index: "do".idx_player_id_firstname_lastname

CREATE INDEX idx_player_id_firstname_lastname
  ON "do".player
  USING btree
  (id , firstname COLLATE pg_catalog."default" , lastname COLLATE pg_catalog."default" );
  
-- Create Table: "do".team

CREATE TABLE "do".team
(
  id bigint NOT NULL,
  descriptor character(4) NOT NULL,
  deleted boolean NOT NULL DEFAULT false,
  agegroup character(10),
  CONSTRAINT pk_team PRIMARY KEY (id )
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE "do".team
  OWNER TO admin;

-- Create Index: "do".idx_team_id_descriptorptor;

CREATE UNIQUE INDEX idx_team_id_descriptor
  ON "do".team
  USING btree
  (id , descriptor COLLATE pg_catalog."default" );
  
-- Create Table: "do".perffeaturecategory

CREATE TABLE "do".perffeaturecategory
(
  id integer NOT NULL,
  nicename character(30) NOT NULL,
  CONSTRAINT pk_perffeaturecategory PRIMARY KEY (id )
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE "do".perffeaturecategory
  OWNER TO admin;

-- Create Index: "do".idx_perffeaturecategory_id_nicename

CREATE UNIQUE INDEX idx_perffeaturecategory_id_nicename
  ON "do".perffeaturecategory
  USING btree
  (id , nicename COLLATE pg_catalog."default" );
  
-- Create Table: "do".perfsubfeaturecategory

CREATE TABLE "do".perfsubfeaturecategory
(
  id integer NOT NULL,
  parent_id integer NOT NULL,
  nicename character(30) NOT NULL,
  CONSTRAINT pk_perffeaturesubcategory PRIMARY KEY (id ),
  CONSTRAINT fk_perffeaturesubcategory_perffeaturecategory FOREIGN KEY (parent_id)
      REFERENCES "do".perffeaturecategory (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE "do".perfsubfeaturecategory
  OWNER TO admin;

-- Create Index: "do".idx_perffeaturesubcategory_id

CREATE INDEX idx_perffeaturesubcategory_id
  ON "do".perfsubfeaturecategory
  USING btree
  (id );

-- Create schema ref
 
  CREATE SCHEMA ref
  AUTHORIZATION admin;
  
 -- Create table ref.playerreference
  
  CREATE TABLE ref.playerreference
(
  id bigint NOT NULL,
  player_id bigint NOT NULL,
  team_id bigint NOT NULL,
  iscurrent boolean NOT NULL DEFAULT true,
  CONSTRAINT pk_playerreference PRIMARY KEY (id ),
  CONSTRAINT fk_player_playerreference FOREIGN KEY (player_id)
      REFERENCES "do".player (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT,
  CONSTRAINT fk_team_playerreference FOREIGN KEY (team_id)
      REFERENCES "do".team (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE ref.playerreference
  OWNER TO admin;

-- Create Index: ref.idx_playerreference_id;

CREATE UNIQUE INDEX idx_playerreference_id
  ON ref.playerreference
  USING btree
  (id );
  
-- Create Table: "do".perfmeasurement

CREATE TABLE "do".perfmeasurement
(
  id bigint NOT NULL,
  playerreference_id bigint NOT NULL,
  value double precision,
  unit integer,
  "timestamp" timestamp without time zone NOT NULL,
  perfsubfeature_id integer NOT NULL,
  remark character varying(500),
  CONSTRAINT pk_perfmeasurement PRIMARY KEY (id ),
  CONSTRAINT fk_perfmeasurement_perfsubfeaturecategory FOREIGN KEY (perfsubfeature_id)
      REFERENCES "do".perfsubfeaturecategory (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT,
  CONSTRAINT fk_perfmeasurement_playerreference FOREIGN KEY (playerreference_id)
      REFERENCES ref.playerreference (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE RESTRICT
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE "do".perfmeasurement
  OWNER TO admin;

-- Create Index: "do".fkidx_perfmeasurement_perfsubfeaturecategory

CREATE INDEX fkidx_perfmeasurement_perfsubfeaturecategory
  ON "do".perfmeasurement
  USING btree
  (perfsubfeature_id );

-- Create Index: "do".fkidx_perfmeasurement_playerref

CREATE INDEX fkidx_perfmeasurement_playerref
  ON "do".perfmeasurement
  USING btree
  (playerreference_id );

-- Create Index: "do".idx_perfmeasurement_id_playerreferenceid

CREATE INDEX idx_perfmeasurement_id_playerreferenceid
  ON "do".perfmeasurement
  USING btree
  (id , playerreference_id );

-- Create schema mt
  
CREATE SCHEMA mt
  AUTHORIZATION admin;
  
-- Create Table: mt.playerdatahistory

CREATE TABLE mt.playerdatahistory
(
  id integer NOT NULL,
  player_id integer NOT NULL,
  weight double precision,
  height integer,
  remark character varying(500),
  "timestamp" timestamp without time zone NOT NULL,
  CONSTRAINT pk_playerdata PRIMARY KEY (id ),
  CONSTRAINT fk_player_id FOREIGN KEY (player_id)
      REFERENCES "do".player (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE,
  autovacuum_enabled=true
);
ALTER TABLE mt.playerdatahistory
  OWNER TO admin;

-- Create Index: mt.idx_playerdatahistory_id_playerid

CREATE INDEX idx_playerdatahistory_id_playerid
  ON mt.playerdatahistory
  USING btree
  (id , player_id );